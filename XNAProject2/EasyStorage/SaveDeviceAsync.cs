﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace EasyStorage
{
    // implements the asynchronous file operations for the SaveDevice.
    public abstract partial class SaveDevice
    {
        /// <summary>
        ///     Defines the hardware thread on which the operations are performed on the Xbox 360.
        /// </summary>
        public static readonly int[] ProcessorAffinity = { 5 };

        // a queue used for recycling our state objects to avoid garbage or boxing when using ThreadPool

        private readonly object pendingOperationCountLock = new object();
        private readonly Queue<FileOperationState> pendingStates = new Queue<FileOperationState>(100);
        private int pendingOperations;

        /// <summary>
        ///     Helper to set processor affinity for a thread.
        /// </summary>
        private void SetProcessorAffinity()
        {
#if XBOX
			Thread.CurrentThread.SetProcessorAffinity(ProcessorAffinity);
#endif
        }

        /// <summary>
        ///     Helper that performs our asynchronous saving.
        /// </summary>
        private void DoSaveAsync(object asyncState)
        {
            // set our processor affinity
            SetProcessorAffinity();

            var fileOperationState = asyncState as FileOperationState;
            Exception error = null;

            // perform the save operation
            try
            {
                Save(fileOperationState.Container, fileOperationState.File, fileOperationState.Action);
            }
            catch (Exception e)
            {
                error = e;
            }

            // construct our event arguments
            var args = new FileActionCompletedEventArgs(error, fileOperationState.UserState);

            // fire our completion event
            if (SaveCompleted != null)
                SaveCompleted(this, args);

            // recycle our state object
            ReturnFileOperationState(fileOperationState);

            // decrement our pending operation count
            PendingOperationsDecrement();
        }

        /// <summary>
        ///     Helper that performs our asynchronous loading.
        /// </summary>
        private void DoLoadAsync(object asyncState)
        {
            // set our processor affinity
            SetProcessorAffinity();

            var state = asyncState as FileOperationState;
            Exception error = null;

            // perform the load operation
            try
            {
                Load(state.Container, state.File, state.Action);
            }
            catch (Exception e)
            {
                error = e;
            }

            // construct our event arguments
            var args = new FileActionCompletedEventArgs(error, state.UserState);

            // fire our completion event
            if (LoadCompleted != null)
                LoadCompleted(this, args);

            // recycle our state object
            ReturnFileOperationState(state);

            // decrement our pending operation count
            PendingOperationsDecrement();
        }

        /// <summary>
        ///     Helper that performs our asynchronous deleting.
        /// </summary>
        private void DoDeleteAsync(object asyncState)
        {
            // set our processor affinity
            SetProcessorAffinity();

            var state = asyncState as FileOperationState;
            Exception error = null;

            // perform the delete operation
            try
            {
                Delete(state.Container, state.File);
            }
            catch (Exception e)
            {
                error = e;
            }

            // construct our event arguments
            var args = new FileActionCompletedEventArgs(error, state.UserState);

            // fire our completion event
            if (DeleteCompleted != null)
                DeleteCompleted(this, args);

            // recycle our state object
            ReturnFileOperationState(state);

            // decrement our pending operation count
            PendingOperationsDecrement();
        }

        /// <summary>
        ///     Helper that performs our asynchronous FileExists.
        /// </summary>
        private void DoFileExistsAsync(object asyncState)
        {
            // set our processor affinity
            SetProcessorAffinity();

            var state = asyncState as FileOperationState;
            Exception error = null;
            var result = false;

            // perform the FileExists operation
            try
            {
                result = FileExists(state.Container, state.File);
            }
            catch (Exception e)
            {
                error = e;
            }

            // construct our event arguments
            var args = new FileExistsCompletedEventArgs(error, result, state.UserState);

            // fire our completion event
            if (FileExistsCompleted != null)
                FileExistsCompleted(this, args);

            // recycle our state object
            ReturnFileOperationState(state);

            // decrement our pending operation count
            PendingOperationsDecrement();
        }

        /// <summary>
        ///     Helper that performs our asynchronous GetFiles.
        /// </summary>
        private void DoGetFilesAsync(object asyncState)
        {
            // set our processor affinity
            SetProcessorAffinity();

            var state = asyncState as FileOperationState;
            Exception error = null;
            string[] result = null;

            // perform the GetFiles operation
            try
            {
                result = GetFiles(state.Container, state.Pattern);
            }
            catch (Exception e)
            {
                error = e;
            }

            // construct our event arguments
            var args = new GetFilesCompletedEventArgs(error, result, state.UserState);

            // fire our completion event
            if (GetFilesCompleted != null)
                GetFilesCompleted(this, args);

            // recycle our state object
            ReturnFileOperationState(state);

            // decrement our pending operation count
            PendingOperationsDecrement();
        }

        /// <summary>
        ///     Helper to increment the pending operation count.
        /// </summary>
        private void PendingOperationsIncrement()
        {
            lock (pendingOperationCountLock)
            {
                pendingOperations++;
            }
        }

        /// <summary>
        ///     Helper to decrement the pending operation count.
        /// </summary>
        private void PendingOperationsDecrement()
        {
            lock (pendingOperationCountLock)
            {
                pendingOperations--;
            }
        }

        /// <summary>
        ///     Helper for getting a FileOperationState object.
        /// </summary>
        private FileOperationState GetFileOperationState()
        {
            lock (pendingStates)
            {
                // recycle any states if we have some available
                if (pendingStates.Count > 0)
                {
                    var state = pendingStates.Dequeue();
                    state.Reset();
                    return state;
                }

                return new FileOperationState();
            }
        }

        /// <summary>
        ///     Helper for returning a FileOperationState to be recycled.
        /// </summary>
        private void ReturnFileOperationState(FileOperationState state)
        {
            lock (pendingStates)
            {
                pendingStates.Enqueue(state);
            }
        }

        #region Nested type: FileOperationState

        /// <summary>
        ///     State object used for our operations.
        /// </summary>
        private class FileOperationState
        {
            public FileAction Action;
            public string Container;
            public string File;
            public string Pattern;
            public object UserState;

            public void Reset()
            {
                Container = null;
                File = null;
                Pattern = null;
                Action = null;
                UserState = null;
            }
        }

        #endregion

        #region IAsyncSaveDevice Members

        /// <summary>
        ///     Gets whether or not the device is busy performing a file operation.
        /// </summary>
        /// <remarks>
        ///     Games can query this property to determine when to show an indication that game is saving
        ///     such as a spinner or other icon.
        /// </remarks>
        public bool IsBusy
        {
            get
            {
                lock (pendingOperationCountLock)
                {
                    return pendingOperations > 0;
                }
            }
        }

        /// <summary>
        ///     Raised when a SaveAsync operation has completed.
        /// </summary>
        public event SaveCompletedEventHandler SaveCompleted;

        /// <summary>
        ///     Raised when a LoadAsync operation has completed.
        /// </summary>
        public event LoadCompletedEventHandler LoadCompleted;

        /// <summary>
        ///     Raised when a DeleteAsync operation has completed.
        /// </summary>
        public event DeleteCompletedEventHandler DeleteCompleted;

        /// <summary>
        ///     Raised when a FileExistsAsync operation has completed.
        /// </summary>
        public event FileExistsCompletedEventHandler FileExistsCompleted;

        /// <summary>
        ///     Raised when a GetFilesAsync operation has completed.
        /// </summary>
        public event GetFilesCompletedEventHandler GetFilesCompleted;

        /// <summary>
        ///     Saves a file asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container in which to save the file.</param>
        /// <param name="fileName">The file to save.</param>
        /// <param name="saveAction">The save action to perform.</param>
        public void SaveAsync(string containerName, string fileName, FileAction saveAction)
        {
            SaveAsync(containerName, fileName, saveAction, null);
        }

        /// <summary>
        ///     Saves a file asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container in which to save the file.</param>
        /// <param name="fileName">The file to save.</param>
        /// <param name="saveAction">The save action to perform.</param>
        /// <param name="userState">A state object used to identify the async operation.</param>
        public void SaveAsync(string containerName, string fileName, FileAction saveAction, object userState)
        {
            // increment our pending operations count
            PendingOperationsIncrement();

            // get a FileOperationState and fill it in
            var state = GetFileOperationState();
            state.Container = containerName;
            state.File = fileName;
            state.Action = saveAction;
            state.UserState = userState;

            // queue up the work item
            ThreadPool.QueueUserWorkItem(DoSaveAsync, state);
        }

        /// <summary>
        ///     Loads a file asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container from which to load the file.</param>
        /// <param name="fileName">The file to load.</param>
        /// <param name="loadAction">The load action to perform.</param>
        public void LoadAsync(string containerName, string fileName, FileAction loadAction)
        {
            LoadAsync(containerName, fileName, loadAction, null);
        }

        /// <summary>
        ///     Loads a file asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container from which to load the file.</param>
        /// <param name="fileName">The file to load.</param>
        /// <param name="loadAction">The load action to perform.</param>
        /// <param name="userState">A state object used to identify the async operation.</param>
        public void LoadAsync(string containerName, string fileName, FileAction loadAction, object userState)
        {
            // increment our pending operations count
            PendingOperationsIncrement();

            // get a FileOperationState and fill it in
            var state = GetFileOperationState();
            state.Container = containerName;
            state.File = fileName;
            state.Action = loadAction;
            state.UserState = userState;

            // queue up the work item
            ThreadPool.QueueUserWorkItem(DoLoadAsync, state);
        }

        /// <summary>
        ///     Deletes a file asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container from which to delete the file.</param>
        /// <param name="fileName">The file to delete.</param>
        public void DeleteAsync(string containerName, string fileName)
        {
            DeleteAsync(containerName, fileName, null);
        }

        /// <summary>
        ///     Deletes a file asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container from which to delete the file.</param>
        /// <param name="fileName">The file to delete.</param>
        /// <param name="userState">A state object used to identify the async operation.</param>
        public void DeleteAsync(string containerName, string fileName, object userState)
        {
            // increment our pending operations count
            PendingOperationsIncrement();

            // get a FileOperationState and fill it in
            var state = GetFileOperationState();
            state.Container = containerName;
            state.File = fileName;
            state.UserState = userState;

            // queue up the work item
            ThreadPool.QueueUserWorkItem(DoDeleteAsync, state);
        }

        /// <summary>
        ///     Determines if a given file exists asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container in which to check for the file.</param>
        /// <param name="fileName">The name of the file.</param>
        public void FileExistsAsync(string containerName, string fileName)
        {
            FileExistsAsync(containerName, fileName, null);
        }

        /// <summary>
        ///     Determines if a given file exists asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container in which to check for the file.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="userState">A state object used to identify the async operation.</param>
        public void FileExistsAsync(string containerName, string fileName, object userState)
        {
            // increment our pending operations count
            PendingOperationsIncrement();

            // get a FileOperationState and fill it in
            var state = GetFileOperationState();
            state.Container = containerName;
            state.File = fileName;
            state.UserState = userState;

            // queue up the work item
            ThreadPool.QueueUserWorkItem(DoFileExistsAsync, state);
        }

        /// <summary>
        ///     Gets an array of all files available in a container asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container in which to search for files.</param>
        public void GetFilesAsync(string containerName)
        {
            GetFilesAsync(containerName, null);
        }

        /// <summary>
        ///     Gets an array of all files available in a container asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container in which to search for files.</param>
        /// <param name="userState">A state object used to identify the async operation.</param>
        public void GetFilesAsync(string containerName, object userState)
        {
            GetFilesAsync(containerName, "*", userState);
        }

        /// <summary>
        ///     Gets an array of all files available in a container that match the given pattern asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container in which to search for files.</param>
        /// <param name="pattern">A search pattern to use to find files.</param>
        public void GetFilesAsync(string containerName, string pattern)
        {
            GetFilesAsync(containerName, pattern, null);
        }

        /// <summary>
        ///     Gets an array of all files available in a container that match the given pattern asynchronously.
        /// </summary>
        /// <param name="containerName">The name of the container in which to search for files.</param>
        /// <param name="pattern">A search pattern to use to find files.</param>
        /// <param name="userState">A state object used to identify the async operation.</param>
        public void GetFilesAsync(string containerName, string pattern, object userState)
        {
            // increment our pending operations count
            PendingOperationsIncrement();

            // get a FileOperationState and fill it in
            var state = GetFileOperationState();
            state.Container = containerName;
            state.Pattern = pattern;
            state.UserState = userState;

            // queue up the work item
            ThreadPool.QueueUserWorkItem(DoGetFilesAsync, state);
        }

        #endregion
    }
}