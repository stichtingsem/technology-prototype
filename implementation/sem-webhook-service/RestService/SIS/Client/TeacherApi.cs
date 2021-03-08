using System;
using System.Collections.Generic;
using RestSharp;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ITeacherApi
    {
        /// <summary>
        /// Get Teacher by ID Retrieve basic teacher information including the groups they teach.  
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schoolYear">Request data for this teacher about a specific school year, if not provided, default to current.</param>
        /// <returns>Teacher</returns>
        Teacher GetTeacherId (string id, string schoolYear);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class TeacherApi : ITeacherApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public TeacherApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherApi"/> class.
        /// </summary>
        /// <returns></returns>
        public TeacherApi(String basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }
    
        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(String basePath)
        {
            this.ApiClient.BasePath = basePath;
        }
    
        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public String GetBasePath(String basePath)
        {
            return this.ApiClient.BasePath;
        }
    
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient {get; set;}
    
        /// <summary>
        /// Get Teacher by ID Retrieve basic teacher information including the groups they teach.  
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schoolYear">Request data for this teacher about a specific school year, if not provided, default to current.</param>
        /// <returns>Teacher</returns>
        public Teacher GetTeacherId (string id, string schoolYear)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetTeacherId");
    
            var path = "/teacher/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (schoolYear != null) queryParams.Add("schoolYear", ApiClient.ParameterToString(schoolYear)); // query parameter
                        
            // authentication setting, if any
            String[] authSettings = new String[] { "APIKey" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetTeacherId: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetTeacherId: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Teacher) ApiClient.Deserialize(response.Content, typeof(Teacher), response.Headers);
        }
    
    }
}
