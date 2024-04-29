using NetArchTest.Rules;
using FluentAssertions;
namespace Bookstore.Tests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Bookstore.Domain";
        private const string ApplicationNamespace = "Bookstore.Application";
        private const string InfrastructureNamespace = "Bookstore.Infrastructure";
        private const string ContractsNamespace = "Bookstore.Contract";
        private const string ApiNamespace = "Bookstore.Api";

        [Fact]
        public void Domain_Should_Not_Have_DependenciesOnOtherProject()
        {
            // Arrange
            var assembly = typeof(Domain.ExternalAssemblyReference).Assembly;

            var otherProjects = new[]
            {
            ApplicationNamespace,
            InfrastructureNamespace,   
            ApiNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_Have_DependenciesOnOtherProject()
        {
            // Arrange
            var assembly = typeof(Bookstore.Application.ExternalAssemblyReference).Assembly;

            var otherProjects = new[]
            {
            InfrastructureNamespace,
            ContractsNamespace,
            ApiNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Infrastructure_Should_Not_Have_DependenciesOnOtherProject()
        {
            // Arrange
            var assembly = typeof(Infrastructure.ExternalAssemblyReference).Assembly;

            var otherProjects = new[]
            {
            ContractsNamespace,
            ApiNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }


        [Fact]
        public void Contract_Should_Not_Have_DependenciesOnOtherProject()
        {
            // Arrange
            var assembly = typeof(Contract.ExternalAssemblyReference).Assembly;

            var otherProjects = new[]
            {
            ApplicationNamespace,
            InfrastructureNamespace,
            DomainNamespace,
            ApiNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Api_Should_Not_Have_DependenciesOnOtherProject()
        {
            // Arrange
            var assembly = typeof(Api.ExternalAssemblyReference).Assembly;

            var otherProjects = new[]
            {            
            InfrastructureNamespace,
            DomainNamespace,            
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}
