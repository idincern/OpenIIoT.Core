﻿/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀
      █
      █      ▄███████▄                                                              ███    █▄
      █     ███    ███                                                              ███    ███
      █     ███    ███   ▄█████   ▄██████    █  █▄     ▄█████     ▄████▄     ▄█████ ███    ███     ██     █   █        █      ██    ▄█   ▄
      █     ███    ███   ██   ██ ██    ██   ██ ▄██▀    ██   ██   ██    ▀    ██   █  ███    ███ ▀███████▄ ██  ██       ██  ▀███████▄ ██   █▄
      █   ▀█████████▀    ██   ██ ██    ▀    ██▐█▀      ██   ██  ▄██        ▄██▄▄    ███    ███     ██  ▀ ██▌ ██       ██▌     ██  ▀ ▀▀▀▀▀██
      █     ███        ▀████████ ██    ▄  ▀▀████     ▀████████ ▀▀██ ███▄  ▀▀██▀▀    ███    ███     ██    ██  ██       ██      ██    ▄█   ██
      █     ███          ██   ██ ██    ██   ██ ▀██▄    ██   ██   ██    ██   ██   █  ███    ███     ██    ██  ██▌    ▄ ██      ██    ██   ██
      █    ▄████▀        ██   █▀ ██████▀    ▀█   ▀█▀   ██   █▀   ██████▀    ███████ ████████▀     ▄██▀   █   ████▄▄██ █      ▄██▀    █████
      █
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █
      █
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀ ▀ ▀▀▀     ▀▀               ▀
      █  The GNU Affero General Public License (GNU AGPL)
      █
      █  Copyright (C) 2016-2017 JP Dillingham (jp@dillingham.ws)
      █
      █  This program is free software: you can redistribute it and/or modify
      █  it under the terms of the GNU Affero General Public License as published by
      █  the Free Software Foundation, either version 3 of the License, or
      █  (at your option) any later version.
      █
      █  This program is distributed in the hope that it will be useful,
      █  but WITHOUT ANY WARRANTY; without even the implied warranty of
      █  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
      █  GNU Affero General Public License for more details.
      █
      █  You should have received a copy of the GNU Affero General Public License
      █  along with this program.  If not, see <http://www.gnu.org/licenses/>.
      █
      ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀  ▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██
                                                                                                   ██
                                                                                               ▀█▄ ██ ▄█▀
                                                                                                 ▀████▀
                                                                                                   ▀▀                            */

using System;
using System.Collections.Generic;
using System.IO;
using NLog;
using NLog.xLogger;
using OpenIIoT.SDK.Package;
using OpenIIoT.SDK.Packaging.Operations;
using OpenIIoT.SDK.Packaging.Manifest;
using OpenIIoT.SDK.Platform;
using Utility.OperationResult;

namespace OpenIIoT.Core.Package
{
    /// <summary>
    ///     Scans a given list of files for Packages and generates and returns a list of found <see cref="IPackage"/> instances.
    /// </summary>
    public class PackageUtility
    {
        #region Private Fields

        /// <summary>
        ///     The Logger for this class.
        /// </summary>
        private static xLogger logger = (xLogger)LogManager.GetCurrentClassLogger(typeof(xLogger));

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PackageUtility"/> class.
        /// </summary>
        /// <param name="platform">The IPlatform instance with which to scan the disk.</param>
        public PackageUtility(IPlatform platform)
        {
            Platform = platform;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        ///     Gets or sets the list of filenames to scan.
        /// </summary>
        public IPlatform Platform { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Creates a Package file from the specified binary data.
        /// </summary>
        /// <param name="data">The binary data containing the Package.</param>
        /// <returns>A Result containing the result of the operation and the created Package.</returns>
        public IResult<IPackage> CreatePackage(byte[] data)
        {
            logger.EnterMethod();
            IResult<IPackage> retVal = new Result<IPackage>();

            logger.Debug($"Creating new Package...");

            string tempFile = Path.Combine(Platform.Directories.Temp, Guid.NewGuid().ToString());

            logger.Debug($"Saving new Package to '{tempFile}'...");

            retVal.Incorporate(Platform.WriteFileBytes(tempFile, data));

            if (retVal.ResultCode != ResultCode.Failure)
            {
                IResult<IPackage> readResult = Read(tempFile);

                retVal.Incorporate(readResult);

                if (retVal.ResultCode != ResultCode.Failure)
                {
                    string destinationFilename = GetPackageFilename(readResult.ReturnValue);

                    retVal.Incorporate(Platform.CopyFile(tempFile, destinationFilename, true));

                    if (retVal.ResultCode != ResultCode.Failure)
                    {
                        retVal.ReturnValue = readResult.ReturnValue;
                        retVal.ReturnValue.Filename = destinationFilename;
                    }
                }
            }

            if (retVal.ResultCode == ResultCode.Failure)
            {
                retVal.AddError("Unable to create Package from supplied data.");
            }

            retVal.LogResult(logger.Debug);
            logger.ExitMethod();
            return retVal;
        }

        /// <summary>
        ///     Reads the specified file and, if it is a valid <see cref="Package"/>, returns an <see cref="IPackage"/> instance
        ///     from the contents.
        /// </summary>
        /// <param name="fileName">The filename of the file to read.</param>
        /// <returns>A Result containing the result of the operation and the created IPackage instance.</returns>
        public IResult<IPackage> Read(string fileName)
        {
            logger.EnterMethod(true);
            IResult<IPackage> retVal = new Result<IPackage>();

            logger.Debug($"Reading Package '{fileName}'...");

            ManifestExtractor extractor = new ManifestExtractor();
            extractor.Updated += (sender, e) => logger.Debug(e.Message);

            PackageManifest manifest;

            try
            {
                manifest = extractor.ExtractManifest(fileName);

                retVal.ReturnValue = GetPackage(fileName, manifest);
            }
            catch (Exception ex)
            {
                retVal.AddError(ex.Message);
                retVal.AddError($"Unable to read Package '{Path.GetFileName(fileName)}'.");
            }

            retVal.LogResult(logger.Debug);
            logger.ExitMethod();

            return retVal;
        }

        /// <summary>
        ///     Scans the specified directory and generates a list of found <see cref="IPackage"/> instances.
        /// </summary>
        /// <param name="directory">The directory to scan.</param>
        /// <returns>A Result containing the result of the operation and the list of found IPackage instances.</returns>
        public IResult<IList<IPackage>> Scan(string directory)
        {
            Guid guid = logger.EnterMethod(true);
            IResult<IList<IPackage>> retVal = new Result<IList<IPackage>>();
            retVal.ReturnValue = new List<IPackage>();

            logger.Debug($"Scanning directory '{directory}'...");

            ManifestExtractor extractor = new ManifestExtractor();
            extractor.Updated += (sender, e) => logger.Debug(e.Message);

            IResult<IList<string>> fileListResult = Platform.ListFiles(directory);
            retVal.Incorporate(fileListResult);

            if (retVal.ResultCode != ResultCode.Failure)
            {
                foreach (string file in fileListResult.ReturnValue)
                {
                    IResult<IPackage> readResult = Read(file);

                    if (readResult.ResultCode != ResultCode.Failure)
                    {
                        retVal.ReturnValue.Add(readResult.ReturnValue);
                    }
                    else
                    {
                        retVal.AddWarning(readResult.GetLastError());
                    }
                }
            }

            retVal.LogResult(logger);
            logger.ExitMethod(guid);

            return retVal;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Creates a <see cref="Package"/> instance with file metadata from the given file and the given
        ///     <see cref="PackageManifest"/> .
        /// </summary>
        /// <param name="fileName">The filename from which to retrieve the Package metadata.</param>
        /// <param name="manifest">The Manifest with which to initialize the <see cref="Package"/> instance.</param>
        /// <returns>The created Package.</returns>
        private IPackage GetPackage(string fileName, PackageManifest manifest)
        {
            FileInfo info = new FileInfo(fileName);

            return new Package(fileName, info.LastWriteTime, manifest);
        }

        /// <summary>
        ///     Creates and returns a valid filename for the specified <see cref="IPackage"/>.
        /// </summary>
        /// <param name="package">The Package for which the filename is to be created.</param>
        /// <returns>The created filename.</returns>
        private string GetPackageFilename(IPackage package)
        {
            string filename = package.FQN + "." + package.Version + PackageConstants.PackageFilenameExtension;

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(c, PackageConstants.PackageFilenameInvalidCharacterSubstitution);
            }

            return Path.Combine(Platform.Directories.Packages, filename);
        }

        #endregion Private Methods
    }
}