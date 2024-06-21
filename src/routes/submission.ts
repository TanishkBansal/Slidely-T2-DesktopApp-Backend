import { Request, Response } from 'express';
import fs from 'fs';
import path from 'path';

const dbFilePath = path.join(__dirname, '../db.json');

// Read all submissions
export const readSubmissions = (req: Request, res: Response) => {
    fs.readFile(dbFilePath, 'utf8', (err, data) => {
        if (err) {
            res.status(500).send('Failed to read database');
            return;
        }
        res.status(200).json(JSON.parse(data));
    });
};

// Save a new submission
export const submitSubmission = (req: Request, res: Response) => {
    const newSubmission = req.body;
    fs.readFile(dbFilePath, 'utf8', (err, data) => {
        if (err) {
            res.status(500).send('Failed to read database');
            return;
        }
        const submissions = JSON.parse(data);
        submissions.push(newSubmission);
        fs.writeFile(dbFilePath, JSON.stringify(submissions), 'utf8', (err) => {
            if (err) {
                res.status(500).send('Failed to save database');
                return;
            }
            res.status(200).send('Submission saved');
        });
    });
};

// Update a submission by index
export const updateSubmission = (req: Request, res: Response) => {
    const index = parseInt(req.params.index as string);
    const updatedSubmission = req.body;

    fs.readFile(dbFilePath, 'utf8', (err, data) => {
        if (err) {
            res.status(500).send('Failed to read database');
            return;
        }
        const submissions = JSON.parse(data);
        if (index >= 0 && index < submissions.length) {
            submissions[index] = updatedSubmission;
            fs.writeFile(dbFilePath, JSON.stringify(submissions), 'utf8', (err) => {
                if (err) {
                    res.status(500).send('Failed to save database');
                    return;
                }
                res.status(200).send('Submission updated');
            });
        } else {
            res.status(404).send('Submission not found');
        }
    });
};

// Delete a submission by index
export const deleteSubmission = (req: Request, res: Response) => {
    const index = parseInt(req.query.index as string);
    fs.readFile(dbFilePath, 'utf8', (err, data) => {
        if (err) {
            res.status(500).send('Failed to read database');
            return;
        }
        const submissions = JSON.parse(data);
        if (index >= 0 && index < submissions.length) {
            submissions.splice(index, 1);
            fs.writeFile(dbFilePath, JSON.stringify(submissions), 'utf8', (err) => {
                if (err) {
                    res.status(500).send('Failed to save database');
                    return;
                }
                res.status(200).send('Submission deleted');
            });
        } else {
            res.status(404).send('Submission not found');
        }
    });
};

// Export the endpoints
import express from 'express';
const router = express.Router();

router.get('/api/read', readSubmissions);
router.post('/api/submit', submitSubmission);
router.put('/api/update/:index', updateSubmission);
router.delete('/api/delete', deleteSubmission);

export default router;
