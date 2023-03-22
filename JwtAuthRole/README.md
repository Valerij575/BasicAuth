# BasicAuth
1. Create a Project in Visual Studio with ASP.Net Core 5.0
2. Add the Services folder where we can add the business logic
3. Add the Authenticate Handler code in order to handle the basic Authentication - BasicAuthenticationHandler
4. After that we can use the Authorize Attibute as header for each method in the controller to have basic setup.
5. Setup basic authentication in swagger in setup.cs file


# Implementing JWT in ASP.NET Core 5.0 Web API

JSON Web Token (JWT) is an open standard used to share information between two parties. 
The information of JWT is encoded as JSON containing claims or signatures.
When creating an API endpoint that executes operations such as creating or deleting important data, 
we don’t want the endpoint to be freely accessible by everyone. 
Using JWT as an authentication method is a way to achieve this.

1. Creating a new project
2. Implementing JWT
    Install “Microsoft.AspNetCore.Authentication.JwtBearer” NuGet package.

3. Add the following line in “appsettings.json”
   
     "Jwt": {
     "Issuer": "Issuer",
     "Audience": "Audience",
     "Key": "bd1a1ccf8095037f361a4d351e7c0de65f0776bfc2f478ea8d312c763bb6caca"
     }
   This will define the configuration which is needed to implement JWT. 
   Notes: The key used in this tutorial are randomly generated for example purposes, 
          Do not use this for the production environment.
          
    Add the following code below “Services.AddControllers();” in “Setup.cs”
    The above code defines the Authentication Schema for JWT which we will use 
      as an authentication method for our API. 
    The code will use the JWT configuration we defined in “appsetting.json”
  
    Then, add the following lines below “app.UseAuthentication();”.
  
  4. Create a “User” Model to store the Username and Password which we will usefor authorization.
  5. Create a new Controller “AuthController” which we will use to authorize the user and return the JWT Token.
     We will “signingCredentials” variable to store SigningCredentials with SymmetricSecurityKey 
     created from our key and use the HmacSha512 security algorithm.
     The variable “subject” is used to store Claims. 
     In This tutorial, we will generate the Claim of Sub and Email from the username.
     Finally, create a “tokenDescriptor” with the previously defined variable. 
     Use JwtSecurityTokenHandler to create a token from the “tokenDescriptor”. 
     Then use the “WriteToken” method to return the string value of the JWT
  6. Add the [Authorize] tag above the Get Method in WeatherForecast Controller
  

