# Pacopolis Polling System
An electronic voting system that allows users to vote once per election. They may log in and see their vote afterwards, but they may not change it once it has been submitted.
An admin may log in and see the vote totals and who has or hasn't voted but they may not see how each user voted.

## Technologies
- React via Next.js
  - This project uses shadcn's open source, reusable next.js components
    - https://ui.shadcn.com/
- C#
  - ASP.NET Core
- Microsoft SQLServer

## How to Install and Run the Project
1) Unzip all files to your chosen location
2) Install the database to localhost by opening sqlQuery/Group9.sql and running it on your local machine to create the database.<br>
   <img width="422" alt="image" src="https://github.com/EthanEckhardt/Group9/assets/77798025/956d10f0-890b-42ad-a862-4e3577f99a33">
4) Open backend-app.sln in Visual Studio 2022
5) Click the Run Project in the top toolbar of Visual Studio<br>
   <img width="305" alt="image" src="https://github.com/EthanEckhardt/Group9/assets/77798025/00629fd5-ca1a-4ca9-983d-0c5c21a72932">
7) Open the frontend-app _folder_ in Visual Studio Code
8) Click Terminal > New Terminal in the top toolbar of VS Code<br>
   <img width="367" alt="image" src="https://github.com/EthanEckhardt/Group9/assets/77798025/6187fa77-2c7d-4a54-9232-bd7c4669b0de">
9) type ```npm run dev``` into the terminal to start the next.js project
   <img width="727" alt="image" src="https://github.com/EthanEckhardt/Group9/assets/77798025/68010481-a247-4c4b-82b8-b7d0116ae8fd">
10) Navigate to http://localhost:3000/ in your browser of choice
11) Your project should be running!<br>
(psst... You can find the credentials for all the users in the Group9.sql file, but here are some to get you started)
### Non Admin User
First Name: Matthew<br>
Last Name:  Donsig<br>
Birthday:   04/08/2003<br>
SSN:        1234<br>
Password:   password
### Admin User
First Name: Eli<br>
Last Name:  Vanto<br>
Birthday:   06/12/1999<br>
SSN:        7694<br>
Password:   MrAdmin!43<br>
