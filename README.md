# GladLive.AuthService.ASP

GladLive is network session service comparable to Xboxlive or Steam. 

GladLive.AuthService.ASP is a web scalable ASP.NET 5 authentication webservice/web-api for the GladLive distributed network and preforms this role by providing:
  - Services Authentication requests for the GladLive distributed network
  - Vertically and horizontally scalable
  - RSA security scheme used for authentication distributed with clients
  - Web and cloud ready

## GladLive Services

GladLive.ProxyLoadBalancer: https://github.com/GladLive/GladLive.ProxyLoadBalancer

GladLive.AuthService.ASP: https://github.com/GladLive/GladLive.AuthService.ASP

## Setup

To use this project you'll first need a couple of things:
  - Visual Studio 2015 RC 2
  - ASP.NET 5
  - DNX/DNVM
  - Add Nuget Feed https://www.myget.org/F/hellokitty/api/v2 in VS (Options -> NuGet -> Package Sources)

## Builds

(CD will be setup in the future; will not be available on NuGet)

##Tests

#### Linux/Mono - Unit Tests
||Debug x86|Debug x64|Release x86|Release x64|
|:--:|:--:|:--:|:--:|:--:|:--:|
|**master**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/GladLive/GladLive.AuthService.ASP.svg?branch=master)](https://travis-ci.org/GladLive/GladLive.AuthService.ASP) |
|**dev**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/GladLive/GladLive.AuthService.ASP.svg?branch=dev)](https://travis-ci.org/GladLive/GladLive.AuthService.ASP)|

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the MIT license with the additional requirement of adding the GladLive splashscreen to your product.
