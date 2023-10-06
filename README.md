**Online Shopping Cart Management System RESTful API with ASP.NET Core and .NET 6**

This project provides an example of how to create a Web API using ASP.NET Core and .NET 6 for an Online Shopping Cart Management System. The Web API is a RESTful service that exposes a set of endpoints for managing issues. Additionally, this solution includes a OAuth2 service and Docker container.

**Getting Started**

To run the application, follow these steps:

1. Clone the repository: git clone https://github.com/Ndipza/SCMSystem
2. Open the solution in Visual Studio or your favorite code editor.
3. Set the SCMSystem project as the startup project.
4. Navigate to the directory where you have downloaded the SCMSystem.  
5. Open a command line in that folder, make sure you can see the docker-compose file.  
6. Run docker-compose up
7. Open a Package Manager Console in Visual Studio
8. Make sure the Default project in Package Manager Console is Data
9. Type - add-migration Initial
10. Type - update-database
11. Navigate to https://localhost:43333/Swagger
   
The RESTful API uses a Miscrosoft SQL Server Management Studio database to store data but you can choose any EF Core provider you like. 

**Testing as admin user**

1. On swagger page Authenticate create an account as admin user on this endpoint api/Authenticate/register-admin
2. Login here api/Authenticate/login
3. Copy the token from the "Response body"
4. Open authentication tab on the top of swagger.
5. type "bearer" space "paste token" like this ex - bearer wretwetwergdsfgsdfgsdgsdg
6. click authorize
7. As admin user you'll be able to use all the endpoints
8. The sequence of adding items
	a, Category (Product)
	b, Product
	c) Payment method
	d) Payment Status
	e, Cart Status
	f, Cart
	g, Cart items
	f, Payment
	

**Testing as account user**

1. On swagger page Authenticate create an account as accunt user on this endpoint api/Authenticate/register-user
2. Login here api/Authenticate/login
3. Copy the token from the "Response body"
4. Open authentication tab on the top of swagger.
5. type "bearer" space "paste token" like this ex - bearer wretwetwergdsfgsdfgsdgsdg
6. click authorize
7. As account user you'll be able to use only two endpoints which are:
	a, Cart
	b, Cart items
	c, Payment

Run the application. The client application will use the Web API endpoints to perform various operations, including creating and updating issues, adding and deleting comments, and retrieving lists of issues.
