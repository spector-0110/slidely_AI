
```markdown
# Form Submission Backend

This is a simple backend application for handling form submissions. It's built with TypeScript, Express, and MongoDB.

## Features

- Create new form submissions
- Retrieve all form submissions
- Delete form submissions

## Prerequisites

- Node.js (v14 or later)
- MongoDB

## Installation

1. Clone the repository:
   ```
   git clone https://github.com/your-username/Form_Backend.git
   cd Form_Backend
   ```

2. Install dependencies:
   ```
   npm install
   ```

3. Create a `.env` file in the root directory and add your MongoDB URI:
   ```
   PORT=3000
   MONGODB_URI=mongodb://localhost:27017/formdb
   ```

## Usage

To run the server in development mode:
```
npm run dev
```

To build and run in production mode:
```
npm run build
npm start
```

## API Endpoints

- POST /api/forms/submit: Create a new form submission
- GET /api/forms/read: Get all form submissions
- DELETE /api/forms/:id: Delete a form submission

## Data Model

Each form submission includes the following fields:

- name (String, required)
- phone (String, required)
- email (String, required)
- github (String, required)
- stopwatchTime (Number, required)

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](https://choosealicense.com/licenses/mit/)
```

Remember to replace "your-username" in the clone URL with your actual GitHub username or the appropriate repository URL.
