# BidOneCoding
Create a small web application that submits a simple form to the backend server
Some of the assumptions made:
- First name is unique
- Able to change last name only when updating
- Only allow alphabets for first name and last name
- First name and last name length must be between 3-20

Technology used:
.NET 5.0 
MVC Web application
xUnit
Moq

Sample test data is in wwwroot\data\people.json

Haven't considered asynchronous programming as it's a simple read/write to json file
