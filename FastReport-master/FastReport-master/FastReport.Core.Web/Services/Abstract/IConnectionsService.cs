﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Web.Services
{
    /// <summary>
    /// Interface for working with connections. Allows to get connection string properties, connected tables, connection types and create a connection string.
    /// </summary>
    /// <remarks>
    /// The interface may change over time
    /// </remarks>
    public interface IConnectionsService
    {
        /// <summary>
        /// Returns JSON string of connection string properties.
        /// </summary>
        /// <param name="isError">Out variable, which helps to determine if an error is returned</param>
        /// <returns>Returns JSON string with connection string properties. If an error is detected, it returns the error text.</returns>
        string GetConnectionStringPropertiesJSON(string connectionType, string connectionString, out bool isError);

        /// <summary>
        /// Create connection string and returns json with this connection string
        /// </summary>
        /// <param name="isError">Out variable, which helps to determine if an error is returned</param>
        /// <returns>Returns JSON with сonnection string. If an error is detected, it returns the error text.</returns>
        string CreateConnectionStringJSON(string connectionType, IFormCollection form, out bool isError);

        /// <summary>
        /// Returns the list of connected tables by connection type and connection string
        /// </summary>
        /// <param name="isError">Returns a bool variable which means whether the error is returned or not</param>
        /// <returns>Returns JSON with connected tables</returns>
        string GetConnectionTables(string connectionType, string connectionString, List<CustomViewModel> customConnections);

        /// <summary>
        /// Returns the list of connection types
        /// </summary>
        List<string> GetConnectionTypes(bool needSqlSupportInfo = false);
    }
}
