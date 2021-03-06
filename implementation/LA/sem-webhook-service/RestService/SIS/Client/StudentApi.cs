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
    public interface IStudentApi
    {
        /// <summary>
        /// Get Student information by ID Retrieve basic student information, including their groups and subject choices.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schoolYear">Request data for this student about a specific school year, if not provided, default to current.</param>
        /// <returns>Student</returns>
        Student GetStudentId (string id, string schoolYear);
        /// <summary>
        /// Get Student delivery information by ID Retrieve privacy sensitive delivery information by ID - this entity is only available to a specific scope.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StudentDelivery</returns>
        StudentDelivery GetStudentIdDeliveryInformation (string id);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class StudentApi : IStudentApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public StudentApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentApi"/> class.
        /// </summary>
        /// <returns></returns>
        public StudentApi(String basePath)
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
        /// Get Student information by ID Retrieve basic student information, including their groups and subject choices.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schoolYear">Request data for this student about a specific school year, if not provided, default to current.</param>
        /// <returns>Student</returns>
        public Student GetStudentId (string id, string schoolYear)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetStudentId");
    
            var path = "/student/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetStudentId: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetStudentId: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Student) ApiClient.Deserialize(response.Content, typeof(Student), response.Headers);
        }
    
        /// <summary>
        /// Get Student delivery information by ID Retrieve privacy sensitive delivery information by ID - this entity is only available to a specific scope.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StudentDelivery</returns>
        public StudentDelivery GetStudentIdDeliveryInformation (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GetStudentIdDeliveryInformation");
    
            var path = "/student/{id}/delivery-information";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "APIKey" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetStudentIdDeliveryInformation: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetStudentIdDeliveryInformation: " + response.ErrorMessage, response.ErrorMessage);
    
            return (StudentDelivery) ApiClient.Deserialize(response.Content, typeof(StudentDelivery), response.Headers);
        }
    
    }
}
