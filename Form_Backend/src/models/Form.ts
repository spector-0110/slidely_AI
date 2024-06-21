import mongoose, { Schema, Document, Model } from 'mongoose';
import cuid from 'cuid';

export interface IForm extends Document {
  name: string;
  phone: string;
  email: string;
  github: string;
  stopwatchTime: number;
  cuid: string;
}

interface IFormModel extends Model<IForm> {
  deleteByCuid(cuid: string): Promise<boolean>;
}

const FormSchema: Schema = new Schema({
  name: { type: String, required: true },
  phone: { type: String, required: true },
  email: { type: String, required: true },
  github: { type: String, required: true },
  stopwatchTime: { type: Number, required: true },
  cuid: { type: String, default: cuid, unique: true }
});

FormSchema.pre('save', function(next) {
  if (!this.cuid) {
    this.cuid = cuid();
  }
  next();
});

// Corrected static method to delete a form by its cuid
FormSchema.statics.deleteByCuid = async function(cuid: string): Promise<boolean> {
  const result = await this.deleteOne({ cuid: cuid });
  return result.deletedCount === 1;
};

export default mongoose.model<IForm, IFormModel>('Form', FormSchema);