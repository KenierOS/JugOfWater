# JugOfWater
This API provides a solution to the classic water jug ​​problem. Given a pair of jugs with specific capacities and a target amount of water, the API returns a sequence of steps to achieve the desired amount in one of the jugs.
## Getting Started

_These instructions will allow you to get a working copy of the project on your local machine for development and testing purposes._

### Prerequisites

_What you need to install the software and how to install them_

Make sure you have the following installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download) - To run the API.
* [Postman](https://www.postman.com/downloads/) - To test the API.

Run the API:
In Visual Studio, click the "Play" button or run the dotnet run command in the terminal.

Test the API with Postman :

Open Postman.
Create a new POST request.
Enter the API URL
In the "Body" tab, select "raw" and then "JSON".
Enter the following JSON with the values ​​for the jug capacities and the target quantity:
JSON
{
"X": ,
"Y": ,
"Z":
}
Click "Send".
You should receive a JSON response with the sequence of steps to resolve the jugs issue.

Unit tests verify the correct functioning of key methods in the Jugswater controller, including:

JarsSolution: Ensures that the logic to find the solution works correctly for different inputs.

MaxCommonDivisor: Verifies that the calculation of the greatest common divisor is correct.

These tests are important to ensure that the API works as expected and to prevent regressions as changes are made to the code.

.NET 8
ASP.NET Core
C#

1.0

Author ✒️
Kenier Otalora - Initial Development - @kenierOS
