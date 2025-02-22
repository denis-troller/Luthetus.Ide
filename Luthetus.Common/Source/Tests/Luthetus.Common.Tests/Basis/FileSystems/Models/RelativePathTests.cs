﻿using Luthetus.Common.RazorLib.FileSystems.Models;
using Microsoft.Extensions.DependencyInjection;
using static Luthetus.Common.Tests.Basis.FileSystems.FileSystemsTestsHelper;

namespace Luthetus.Common.Tests.Basis.FileSystems.Models;

/// <summary>
/// <see cref="RelativePath"/>
/// </summary>
public class RelativePathTests
{
    /// <summary>
    /// <see cref="RelativePath(string, bool, IEnvironmentProvider)"/>
    /// <see cref="RelativePath.PathType"/>
    /// <see cref="RelativePath.IsDirectory"/>
    /// <see cref="RelativePath.EnvironmentProvider"/>
    /// <see cref="RelativePath.AncestorDirectoryBag"/>
    /// <see cref="RelativePath.NameNoExtension"/>
    /// <see cref="RelativePath.ExtensionNoPeriod"/>
    /// <see cref="RelativePath.UpDirDirectiveCount"/>
    /// <see cref="RelativePath.ExactInput"/>
    /// <see cref="RelativePath.Value"/>
    /// <see cref="RelativePath.NameWithExtension"/>
    /// </summary>
    [Fact]
    public void Constructor()
    {
        InitializeFileSystemsTests(
            out InMemoryEnvironmentProvider environmentProvider,
            out InMemoryFileSystemProvider fileSystemProvider,
            out ServiceProvider serviceProvider);

        {
            // This path is silly because it currently is going one UpDir directive too many
            // just to arive at the same ending location.
            //
            // That being said, I need to parse more than one UpDir directive, and
            // am using this for that purpose.
            var relativePathString = "../../Homework/Math/";
            var relativePath = new RelativePath(relativePathString, true, environmentProvider);

            Assert.Equal(PathType.RelativePath, relativePath.PathType);
            Assert.True(relativePath.IsDirectory);
            Assert.Equal(environmentProvider, relativePath.EnvironmentProvider);
            Assert.Equal("Math", relativePath.NameNoExtension);
            Assert.Equal("/", relativePath.ExtensionNoPeriod);
            Assert.Equal(2, relativePath.UpDirDirectiveCount);
            Assert.Equal(relativePathString, relativePath.ExactInput);
            Assert.Equal(relativePathString, relativePath.Value);
            Assert.Equal("Math/", relativePath.NameWithExtension);

            var homeworkDirectory = relativePath.AncestorDirectoryBag[0];
            Assert.Equal("Homework", homeworkDirectory.NameNoExtension);
        }

        {
            var relativePathString = "../Math/addition.txt";
            var relativePath = new RelativePath(relativePathString, false, environmentProvider);

            Assert.Equal(PathType.RelativePath, relativePath.PathType);
            Assert.False(relativePath.IsDirectory);
            Assert.Equal(environmentProvider, relativePath.EnvironmentProvider);
            Assert.Equal("addition", relativePath.NameNoExtension);
            Assert.Equal("txt", relativePath.ExtensionNoPeriod);
            Assert.Equal(1, relativePath.UpDirDirectiveCount);
            Assert.Equal(relativePathString, relativePath.ExactInput);
            Assert.Equal(relativePathString, relativePath.Value);
            Assert.Equal("addition.txt", relativePath.NameWithExtension);

            var mathDirectory = relativePath.AncestorDirectoryBag[0];
            Assert.Equal("Math", mathDirectory.NameNoExtension);
        }

        {
            var relativePathString = "./skeletalSystem.txt";
            var relativePath = new RelativePath(relativePathString, false, environmentProvider);

            Assert.Equal(PathType.RelativePath, relativePath.PathType);
            Assert.False(relativePath.IsDirectory);
            Assert.Equal(environmentProvider, relativePath.EnvironmentProvider);
            Assert.Equal("skeletalSystem", relativePath.NameNoExtension);
            Assert.Equal("txt", relativePath.ExtensionNoPeriod);
            Assert.Equal(0, relativePath.UpDirDirectiveCount);
            Assert.Equal(relativePathString, relativePath.ExactInput);
            Assert.Equal(relativePathString, relativePath.Value);
            Assert.Equal("skeletalSystem.txt", relativePath.NameWithExtension);

            Assert.Empty(relativePath.AncestorDirectoryBag);
        }

        {
            var relativePathString = "../";
            var relativePath = new RelativePath(relativePathString, true, environmentProvider);

            Assert.Equal(PathType.RelativePath, relativePath.PathType);
            Assert.True(relativePath.IsDirectory);
            Assert.Equal(environmentProvider, relativePath.EnvironmentProvider);
            Assert.Equal(string.Empty, relativePath.NameNoExtension);
            Assert.Equal("/", relativePath.ExtensionNoPeriod);
            Assert.Equal(1, relativePath.UpDirDirectiveCount);
            Assert.Equal(relativePathString, relativePath.ExactInput);
            Assert.Equal(relativePathString, relativePath.Value);
            Assert.Equal("/", relativePath.NameWithExtension);

            Assert.Empty(relativePath.AncestorDirectoryBag);
        }

        {
            var relativePathString = "../";
            var relativePath = new RelativePath(relativePathString, true, environmentProvider);

            Assert.Equal(PathType.RelativePath, relativePath.PathType);
            Assert.True(relativePath.IsDirectory);
            Assert.Equal(environmentProvider, relativePath.EnvironmentProvider);
            Assert.Equal(string.Empty, relativePath.NameNoExtension);
            Assert.Equal("/", relativePath.ExtensionNoPeriod);
            Assert.Equal(1, relativePath.UpDirDirectiveCount);
            Assert.Equal(relativePathString, relativePath.ExactInput);
            Assert.Equal(relativePathString, relativePath.Value);
            Assert.Equal("/", relativePath.NameWithExtension);

            Assert.Empty(relativePath.AncestorDirectoryBag);
        }
    }
}