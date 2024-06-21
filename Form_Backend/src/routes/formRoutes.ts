import express, { Request, Response } from 'express';
import Form, { IForm } from '../models/Form';

const router = express.Router();


router.post('/submit', async (req: Request, res: Response) => {
  try {
    const form: IForm = new Form(req.body);
    await form.save();
    res.status(201).json(form);
  } catch (error) {
    res.status(400).json({ message: 'Error creating form submission', error });
  }
});

router.get('/read', async (req: Request, res: Response) => {
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


router.put('/:id', async (req: Request, res: Response) => {
  try {
    const form: IForm | null = await Form.findByIdAndUpdate(req.params.id, req.body, { new: true });
    if (!form) {
      return res.status(404).json({ message: 'Form in submission not found' });
    }
    res.json(form);
  } catch (error) {
    res.status(400).json({ message: 'Error updating form submission', error });
  }
});

router.delete('/bycuid/:cuid', async (req: Request, res: Response) => {
  try {
    const deleted = await Form.deleteByCuid(req.params.cuid);
    if (deleted) {
      res.json({ message: 'Form submission deleted successfully' });
    } else {
      res.status(404).json({ message: 'Form submission not found' });
    }
  } catch (error) {
    res.status(500).json({ message: 'Error in deleting form submission', error });
  }
});

export default router;