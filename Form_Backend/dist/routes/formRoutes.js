"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const Form_1 = __importDefault(require("../models/Form"));
const router = express_1.default.Router();
router.post('/', (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const form = new Form_1.default(req.body);
        yield form.save();
        res.status(201).json(form);
        console.log("done");
    }
    catch (error) {
        res.status(400).json({ message: 'Error creating form submission', error });
    }
}));
router.get('/', (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const forms = yield Form_1.default.find();
        res.json(forms);
    }
    catch (error) {
        res.status(500).json({ message: 'Error fetching form submissions', error });
    }
}));
router.get('/:id', (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const form = yield Form_1.default.findById(req.params.id);
        if (!form) {
            return res.status(404).json({ message: 'Form submission not found' });
        }
        res.json(form);
    }
    catch (error) {
        res.status(500).json({ message: 'Error in fetching form submission', error });
    }
}));
router.put('/:id', (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const form = yield Form_1.default.findByIdAndUpdate(req.params.id, req.body, { new: true });
        if (!form) {
            return res.status(404).json({ message: 'Form in submission not found' });
        }
        res.json(form);
    }
    catch (error) {
        res.status(400).json({ message: 'Error updating form submission', error });
    }
}));
router.delete('/:id', (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const form = yield Form_1.default.findByIdAndDelete(req.params.id);
        if (!form) {
            return res.status(404).json({ message: 'Form submission not found' });
        }
        res.json({ message: 'Form submission deleted successfully' });
    }
    catch (error) {
        res.status(500).json({ message: 'Error in deleting form submission', error });
    }
}));
exports.default = router;
