
import express, { Request, Response } from 'express';
import Form, { IForm } from '../models/Form';

const router = express.Router();


router.post('/', async (req: Request, res: Response) => {
  try {
    const form: IForm = new Form(req.body);
    await form.save();
    res.status(201).json(form);
  } catch (error) {
    res.status(400).json({ message: 'Error creating form submission', error });
  }
});

router.get('/', async (req: Request, res: Response) => {
  try {
    const forms: IForm[] = await Form.find();
    res.json(forms);
  } catch (error) {
    res.status(500).json({ message: 'Error fetching form submissions', error });
  }
});


router.get('/:id', async (req: Request, res: Response) => {
  try {
    const form: IForm | null = await Form.findById(req.params.id);
    if (!form) {
      return res.status(404).json({ message: 'Form submission not found' });
    }
    res.json(form);
  } catch (error) {
    res.status(500).json({ message: 'Error in fetching form submission', error });
  }
});



export default router;