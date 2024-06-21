import express from 'express';
import bodyParser from 'body-parser';
import fs from 'fs';
import path from 'path';
import submissionRouter from './routes/submission';

const app = express();
const port = 3000;

app.use(bodyParser.json());

app.get('/ping', (req, res) => {
  res.send(true);
});

app.use('/api', submissionRouter);

app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`);
});
