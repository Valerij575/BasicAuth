# JWT Authentication and role base uthorization in .net 7
1. Create a Project in Visual Studio with ASP.Net web API 7.0
2. Add Data layer


# Implementing JWT in ASP.NET Core 5.0 Web API

JSON Web Token (JWT) is an open standard used to share information between two parties. 
The information of JWT is encoded as JSON containing claims or signatures.
When creating an API endpoint that executes operations such as creating or deleting important data, 
we don’t want the endpoint to be freely accessible by everyone. 
Using JWT as an authentication method is a way to achieve this.

1. Creating a new project
2. Implementing NuGet packages to web API
    Install “Microsoft.AspNetCore.Authentication.JwtBearer” 
    Install "Microsoft.EntityFramework.Design"

3. Implementing NuGet packages Data layer
     “Microsoft.EntityFramework.SqlServer” 
     “Microsoft.EntityFramework.Tools”
     "Microsoft.AspNetCore.Identity.EntityFrameworkCore"

4. Add the following line in “appsettings.json”
   
     "Jwt": {
    "ValidAudience": "https://localhost:7060",
    "ValidIssuer": "https://localhost:7060",
    "Secret": "bd1a1ccf8095037f361a4d351e7c0de65f0776bfc2f478ea8d312c763bb6caca"
  },

   This will define the configuration which is needed to implement JWT. 
   
          
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
  

