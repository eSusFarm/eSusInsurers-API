using System;
using System.Linq;
using System.Reflection;
using eSusInsurers.Common.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class SqlServerConstraintExceptionHandlerTests
{
    [Fact]
    public void HandleDbUpdateExceptionForDuplicateKey_ShouldThrowSqlDbUpdateException_ForDuplicateKey()
    {
        // Arrange
        var sqlException = CreateSqlException(2601, "Violation of UNIQUE KEY constraint. The duplicate key value is (ABC123).");
        var dbUpdateException = new DbUpdateException("DB update failed", (Exception?)sqlException);

        // Act
        Action act = () => SqlServerConstraintExceptionHandler.HandleDbUpdateExceptionForDuplicateKey(dbUpdateException);

        // Assert
        var ex = Assert.Throws<SqlDbUpdateException>(act);
        Assert.Contains("Duplicate key value found: ABC123", ex.Message);
    }

    [Fact]
    public void HandleDbUpdateExceptionForDuplicateKey_ShouldThrowSqlDbUpdateException_ForOtherSqlError()
    {
        // Arrange
        var sqlException = CreateSqlException(547, "Foreign key violation.");
        var dbUpdateException = new DbUpdateException("FK error", (Exception?)sqlException);

        // Act
        Action act = () => SqlServerConstraintExceptionHandler.HandleDbUpdateExceptionForDuplicateKey(dbUpdateException);

        // Assert
        var ex = Assert.Throws<SqlDbUpdateException>(act);
        Assert.Contains("FK error", ex.Message);
    }

    [Fact]
    public void HandleDbUpdateExceptionForDuplicateKey_ShouldThrowSqlDbUpdateException_ForNonSqlException()
    {
        // Arrange
        var inner = new InvalidOperationException("Not a SQL exception");
        var dbUpdateException = new DbUpdateException("Some other issue", inner);

        // Act
        Action act = () => SqlServerConstraintExceptionHandler.HandleDbUpdateExceptionForDuplicateKey(dbUpdateException);

        // Assert
        var ex = Assert.Throws<SqlDbUpdateException>(act);
        Assert.Contains("Some other issue", ex.Message);
    }

    private static SqlException CreateSqlException(int number, string message)
    {
        // Create a SqlError instance
        var sqlErrorCtor = typeof(SqlError)
            .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
            .First(c => c.GetParameters().Length == 8);

        var sqlError = (SqlError)sqlErrorCtor.Invoke(new object[]
        {
            number, (byte)0, (byte)0, "server", message, "proc", 0, null
        });

        // Create SqlErrorCollection and add the SqlError to it
        var errorCollection = (SqlErrorCollection)Activator.CreateInstance(typeof(SqlErrorCollection), true)!;

        typeof(SqlErrorCollection)
            .GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance)!
            .Invoke(errorCollection, new object[] { sqlError });

        // Get the correct SqlException.CreateException method
        var createExceptionMethod = typeof(SqlException)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .First(m =>
            {
                var parameters = m.GetParameters();
                return m.Name == "CreateException"
                       && parameters.Length == 2
                       && parameters[0].ParameterType == typeof(SqlErrorCollection)
                       && parameters[1].ParameterType == typeof(string);
            });

        return (SqlException)createExceptionMethod.Invoke(null, new object[] { errorCollection, "7.0.0" })!;
    }

}
