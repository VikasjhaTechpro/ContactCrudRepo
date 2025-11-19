ContactApp.Mvc - Separate MVC UI project (Option A - Axios client calls)

How to run:
1. Ensure your API is running at https://localhost:57748 (per your setting).
2. From this folder, run:
   dotnet restore
   dotnet run
3. Open https://localhost:5001 or the URL shown by dotnet run (MVC app)
Note: The frontend is configured to call the API at https://localhost:57748/api/contacts
