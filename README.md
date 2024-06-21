# Slidely Form App

This is a Windows Forms application developed using Visual Basic (.NET) that allows users to create, view, and delete submissions. The application connects to a MongoDB database to store and manage the submissions.

## Features

- **Create Submissions:** Users can fill in their details and submit them to the database.
- **View Submissions:** Users can navigate through the submissions stored in the database.
- **Delete Submissions:** Users can delete a selected submission from the database.

## Screenshots

### Main Form
The main form provides options to either create a new submission or view existing submissions.

![Main Form](path/to/your/images/main_form.jpeg)

### Create Submission
Users can fill out their details and submit them to the database. The stopwatch feature can be toggled to record the submission time.

![Create Submission](path/to/your/images/create_submission.jpeg)

### View Submissions
Users can view details of the submissions stored in the database. They can navigate through the submissions using the previous and next buttons, and delete a selected submission.

![View Submissions](path/to/your/images/view_submissions.jpeg)
![Submission Deleted](path/to/your/images/submission_deleted.jpeg)

### MongoDB Database
Submissions are stored in a MongoDB database.

![MongoDB](path/to/your/images/mongodb.jpeg)

## Requirements

- .NET Framework 4.8 or higher
- MongoDB

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/yourrepository.git
    ```

2. Open the solution in Visual Studio:
    ```bash
    cd yourrepository
    start YourSolution.sln
    ```

3. Restore the NuGet packages.

4. Update the MongoDB connection string in `App.config` or `Settings.settings`.

5. Build and run the project.

## Usage

1. **Create a new submission:**
   - Click on the "Create New Submission" button.
   - Fill in your details and submit.

2. **View submissions:**
   - Click on the "View Submissions" button.
   - Use the previous and next buttons to navigate through submissions.

3. **Delete a submission:**
   - While viewing a submission, click on the "Delete" button to remove it from the database.

## Contributing

1. Fork the repository.
2. Create a new branch:
    ```bash
    git checkout -b feature-branch
    ```
3. Commit your changes:
    ```bash
    git commit -m 'Add some feature'
    ```
4. Push to the branch:
    ```bash
    git push origin feature-branch
    ```
5. Create a new Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
