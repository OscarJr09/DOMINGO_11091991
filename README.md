# DOMINGO_11091991

# Before building and running the project ensure to have the following packages installed:
  1. CsvHelper version 33.1.0
  2. Microsoft.VisualStudio.Azure.Containers.Tools.Targets version 1.20.1
  3. Swashbuckle.AspNetCore 6.4.0

# Once project has been built and executed it will open up Swagger UI
  - From Swagger UI Authorize the request using the stored API Key for the file upload authentication (The API Key can be        found in appsettings.json file (FileUpload:ApiKey))
  - Use /api/FileProcessor/FileUpload end point to upload csv file. Use the sample file "products-100.csv" found in              DOMINGO_11091991/Sample CSV for testing. Please note that the endpoint only support ".csv" extension.
  - The file tracking is being stored in a json file named "FileUploadTracking.json" that can be found inside FileProcessor      folder

# Alternatively from Postman, you can test /api/FileProcessor/FileUpload using POST request with the following details:
  - On Headers set the following key value pair:
	    Content-Type: multipart/form-data
	    X-Api-Key: {FileUpload:ApiKey from appsettings.json file}
    
  - On Body use form-data and set the following key value pair:
	    file: {csv file to be uploaded}
	    type: text/csv
