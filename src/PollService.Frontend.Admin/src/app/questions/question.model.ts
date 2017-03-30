import { questionType } from "./question-type.model";

export class Question { 
    public id: any;
    public surveyId: any;
    public name: string;
    public body: string;
    public questionType: questionType = questionType.default;
    public description: string;
    public orderIndex: any;
}
