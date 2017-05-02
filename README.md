# GladLive.Authentication

GladLive is network session service comparable to Xboxlive or Steam. 

GladLive.Authentication is the module libraries for authentication/authorization for the GladLive distributed network and preforms this role by providing:
  - Services Authentication requests for the GladLive distributed network
  - Vertically and horizontally scalable
  - Issues JWT tokens to authenticated users
  - Supports HTTPS and signing JWT's with x509 cert
  - Web and cloud ready

## Setup

To use this project you'll first need a couple of things:
  - Visual Studio 2017
  - ASP Core Release
  - Add Nuget Feed https://www.myget.org/F/hellokitty/api/v2 in VS (Options -> NuGet -> Package Sources)

## How To Use

Register the modules in the modules.json and the connection string in the database.json.

Start the application and connect to the {baseurl}/api/AuthenticationRequest endpoint to be issued a JWT authorization response. To authenticate you must send username, password and grant_type password in the request body of a POST with a url encoded content type.

For example: username=TestUsername&password=Test123&grant_type=password

The server will issue a JWT token in the response body or error information.

Also contains a test only account creation controller at {baseurl}/api/AccountCreateRequest using query string parameters.

## Builds

(CD will be setup in the future; will not be available on NuGet)

##Tests

#### Linux/Mono - Unit Tests

TODO

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the MIT license with the additional requirement of adding the GladLive splashscreen to your product.
