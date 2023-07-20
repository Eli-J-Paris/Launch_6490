# Week 3 Assessment

### Setup
* In Package Manager Console
* `drop-database` and then
* `update-database`

## Exercise

Your goal for this assessment is to have an application that allows a user to do the following:
* Create a State by submitting a Form
* See all the cities for a specific state (city index page)
* Create a City for a State by submitting a form

### Creating a State (3 points)

Update the application so that a user can create a State by submitting a form.
* You will need to thoroughly test this functionality. 
* Do not break any currently passing tests.

### City Index Page (3 points)

Update the application so that a user could visit "/states/1/cities" to view all of the cities for a state (in this case, the state with id 1).
* You will need to thoroughly test this functionality. 
* Do not break any currently passing tests.

### Create a City (9 points)

Update the application so that the pre-existing CityCRUD tests will pass.
* There is some starter code that is not yet working.
* You may change any code in the Controllers, Views, and Models.
* You may NOT change the pre-existing tests.

## Questions (5 points)

Edit this file with your answers.

1. Create a Diagram of the Request/Response cycle that would occur when a user creates a city.  Include as much detail as possible!  **Send and image/screenshot of your diagram to your instructors via slack.** (2 points)

2. How does a form submission know what request should be made? Use examples.
A form knows what re quest should be made by the Method atrribute if its Get it will be a get request and if its Post is should redirect the request so information is added to the database.
3. Imagine you are explaining how to create a resource to a co-worker.  How would you describe how the controller action `Create` works?
Create is the actual action of information being added to the database
4. In our State creation functionality - what would happen if a user did not enter an Abbreviation before submitting the form?
An Abbreivation would not be returned and added to the database

## Rubric

This assessment has a total of 20 points.  A score of 15 or higher is a pass.

As a reminder, this assessment is for students and instructors to determine if there are any areas that need additional reinforcement!
