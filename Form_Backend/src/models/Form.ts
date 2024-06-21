import mongoose, { Schema, Document } from 'mongoose';

export interface IForm extends Document {
  name: string;
  phone: string;
  email: string;
  github: string;
  stopwatchTime: number;
}

const FormSchema: Schema = new Schema({
  name: { type: String, required: true },
  phone: { type: String, required: true },
  email: { type: String, required: true },
  github: { type: String, required: true },
  stopwatchTime: { type: Number, required: true }
});

export default mongoose.model<IForm>('Form', FormSchema);