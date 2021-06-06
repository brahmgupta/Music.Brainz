
# Music Brainz Microservice
This is a small **.Net Core http api** application with two endpoints. 
When supplied with the artist / band name it will search the MusicBrainz.org service and
return a response. The return pay load is collection of possible artist matches, if there is more than one possible match. It will return an artist along with collection of all the releases for an artist if there is a single artist with that name.

### Development Tools
 - .NET Core 3.1 with Visual Studio 2019
 - **MediatR**,  **CQRS** & **Dependecy Injection** pattern using **SOLID** Object Oriented principles
 - **Automapper** to transform domain objects to Response models
 - **FluentResults** package to handle API response
 - Tests are written using **Xunit**, **NSubstitute** and **AutoFixture**
 - Documentation is supported by  **SWAGGER**
 - **Logging** for Error, Information and Trace logs.

### Hosting 
This application is hosted on **Azure cloud** - [https://musicbrainzapi.azurewebsites.net/swagger/](https://musicbrainzapi.azurewebsites.net/swagger/) 

 1. Search Artist by query - [https://musicbrainzapi.azurewebsites.net/api/Artist?query={query}](https://musicbrainzapi.azurewebsites.net/api/Artist?query=%7Bquery%7D)
 2. GET Artist by Id - [https://musicbrainzapi.azurewebsites.net/api/Artist/{id}](https://musicbrainzapi.azurewebsites.net/api/Artist/%7Bid%7D)

### Running application
 - Clone this solution and run using VS 2019.
 - It will run on localhost using port 17732 - [http://localhost:17732/](http://localhost:17733/)
 - Swagger End point - [http://localhost:17732/swagger](http://localhost:17733/swagger)
 
 ### End Points
####  1. Search Artist(s) by name - `http://localhost:17732/api/Artist/?query={query}`
When supplied with the artist / band name it will search the MusicBrainz.org service and
return a response.
The return pay load is collection of possible artist matches, if there is more than one possible match. 
It will return an artist along with collection of all the releases for an artist if there is a single artist with that name.

##### 1.1 Sample response for multiple matches

````json
{
  "count": 1948,
  "offset": 0,
  "artists": [
    {
      "id": "4dbf5678-7a31-406a-abbe-232f8ac2cd63",
      "name": "Bryan Adams",
      "country": "CA",
      "gender": "male",
      "releases": []
    },
    {
      "id": "7dfb837e-ca75-4b85-8933-a0421028cf7d",
      "name": "Bryan Adams's Band",
      "country": null,
      "gender": null,
      "releases": []
    }
  ]
}
````

##### 1.2 Sample response for single match

````json
{
   "id":"4dbf5678-7a31-406a-abbe-232f8ac2cd63",
   "name":"Bryan Adams",
   "country":"CA",
   "gender":"Male",
   "releases":[
      {
         "id":"625b7b3e-dda5-404b-883b-1c0b3af061cf",
         "title":"Let Me Take You Dancing",
         "country":"CA",
         "date":"1979",
         "status":"Official"
      },
      {
         "id":"fecc8a9c-1465-43cb-8fa0-7d8a1e1b2661",
         "title":"You Want It ▪ You Got It",
         "country":"JP",
         "date":"1981",
         "status":"Official"
      }
   ]
}
````

####  2. LookUp Artist by Id - `http://localhost:17732/api/Artist/{id}`
When supplied with the `id` it will look up artist with Id and return a response with Artist details with collection of all the releases.

##### Sample JSON Response:
````json
{
   "id":"4dbf5678-7a31-406a-abbe-232f8ac2cd63",
   "name":"Bryan Adams",
   "country":"CA",
   "gender":"Male",
   "releases":[
      {
         "id":"625b7b3e-dda5-404b-883b-1c0b3af061cf",
         "title":"Let Me Take You Dancing",
         "country":"CA",
         "date":"1979",
         "status":"Official"
      },
      {
         "id":"fecc8a9c-1465-43cb-8fa0-7d8a1e1b2661",
         "title":"You Want It ▪ You Got It",
         "country":"JP",
         "date":"1981",
         "status":"Official"
      }
   ]
}
````

