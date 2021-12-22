using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using WebApi.Services;
using FluentAssertions;
using System;

namespace UnitTest
{
    public class FilevalidationTest
    {
        FileService fileService;
        [SetUp]
        public void Setup()
        {
            fileService = new FileService();
        }

        [Test]
        public void EmptyFiletest()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\Emptyfile.txt");
            Stream stream = File.OpenRead(path);
            string json = fileService.FileValidation(stream);
            FileValidationResponse fileValidationResponse = JsonConvert.DeserializeObject<FileValidationResponse>(json);
            fileValidationResponse.Should().NotBeNull();
            fileValidationResponse.fileValid.Should().BeFalse();
            fileValidationResponse.invalidLines.Should().BeNull();
        }
        [Test]
        public void InValidFiletest()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\InValidfile.txt");
            Stream stream = File.OpenRead(path);
            string json = fileService.FileValidation(stream);
            FileValidationResponse fileValidationResponse = JsonConvert.DeserializeObject<FileValidationResponse>(json);
            fileValidationResponse.Should().NotBeNull();
            fileValidationResponse.fileValid.Should().BeFalse();
            fileValidationResponse.invalidLines.Should().NotBeNull();
        }

        [Test]
        public void ValidFiletest()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\Validfile.txt");
            Stream stream = File.OpenRead(path);
            string json = fileService.FileValidation(stream);
            FileValidationResponse fileValidationResponse = JsonConvert.DeserializeObject<FileValidationResponse>(json);
            fileValidationResponse.Should().NotBeNull();
            fileValidationResponse.fileValid.Should().BeTrue();
            fileValidationResponse.invalidLines.Should().BeNull();
        }

      
    }
}