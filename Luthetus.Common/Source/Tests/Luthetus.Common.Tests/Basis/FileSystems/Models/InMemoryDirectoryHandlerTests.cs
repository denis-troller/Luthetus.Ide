﻿using Luthetus.Common.RazorLib.FileSystems.Models;
using Microsoft.Extensions.DependencyInjection;
using static Luthetus.Common.RazorLib.FileSystems.Models.InMemoryFileSystemProvider;
using static Luthetus.Common.Tests.Basis.FileSystems.FileSystemsTestsHelper;

namespace Luthetus.Common.Tests.Basis.FileSystems.Models;

/// <summary>
/// <see cref="InMemoryFileSystemProvider.Directory"/>
/// </summary>
public class InMemoryDirectoryHandlerTests
{
    [Fact]
    public void Constructor()
    {
        /*
        public InMemoryDirectoryHandler(
            InMemoryFileSystemProvider inMemoryFileSystemProvider, IEnvironmentProvider environmentProvider)
         */

        FileSystemsTestsHelper.InitializeFileSystemsTests(
            out InMemoryEnvironmentProvider environmentProvider,
            out InMemoryFileSystemProvider fileSystemProvider,
            out ServiceProvider serviceProvider);

        // This assertion presumes that FileSystemsTestsHelper.InitializeFileSystemsTests
        // is returning as an out variable, an instance of InMemoryDirectoryHandler
        Assert.IsType<InMemoryDirectoryHandler>(fileSystemProvider);
    }

    [Fact]
    public async Task ExistsAsync()
    {
        /*
        public Task<bool> ExistsAsync(
            string absolutePathString, CancellationToken cancellationToken = default)
         */

        InitializeFileSystemsTests(
            out InMemoryEnvironmentProvider environmentProvider,
            out InMemoryFileSystemProvider fileSystemProvider,
            out ServiceProvider serviceProvider);

        Assert.True(await fileSystemProvider.Directory.ExistsAsync(WellKnownPaths.Directories.Math));
        Assert.False(await fileSystemProvider.Directory.ExistsAsync(WellKnownPaths.Directories.NonExistingDirectory));

        // This is false because, 'WellKnownPaths.Files.AdditionTxt' is a file
        Assert.False(await fileSystemProvider.Directory.ExistsAsync(WellKnownPaths.Files.AdditionTxt));
        Assert.False(await fileSystemProvider.Directory.ExistsAsync(WellKnownPaths.Files.NonExistingFile));
    }

    [Fact]
    public void CreateDirectoryAsync()
    {
        /*
        public async Task CreateDirectoryAsync(
            string absolutePathString, CancellationToken cancellationToken = default)
         */

        throw new NotImplementedException();
    }

    [Fact]
    public void DeleteAsync()
    {
        /*
        public async Task DeleteAsync(
            string absolutePathString, bool recursive, CancellationToken cancellationToken = default)
         */

        throw new NotImplementedException();
    }

    [Fact]
    public void CopyAsync()
    {
        /*
        public async Task CopyAsync(
            string sourceAbsoluteFileString, string destinationAbsolutePathString, CancellationToken cancellationToken = default)
         */

        throw new NotImplementedException();
    }

    [Fact]
    public void MoveAsync()
    {
        /*
        public async Task MoveAsync(
            string sourceAbsolutePathString, string destinationAbsolutePathString, CancellationToken cancellationToken = default)
         */

        throw new NotImplementedException();
    }

    [Fact]
    public void GetDirectoriesAsync()
    {
        /*
        public Task<string[]> GetDirectoriesAsync(
            string absolutePathString, CancellationToken cancellationToken = default)
         */

        throw new NotImplementedException();
    }

    [Fact]
    public void GetFilesAsync()
    {
        /*
        public Task<string[]> GetFilesAsync(
            string absolutePathString, CancellationToken cancellationToken = default)
         */

        throw new NotImplementedException();
    }

    [Fact]
    public void EnumerateFileSystemEntriesAsync()
    {
        /*
        public async Task<IEnumerable<string>> EnumerateFileSystemEntriesAsync(
            string absolutePathString, CancellationToken cancellationToken = default)
         */

        throw new NotImplementedException();
    }
}