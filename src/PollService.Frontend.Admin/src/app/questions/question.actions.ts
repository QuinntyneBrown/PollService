import { Question } from "./question.model";

export const questionActions = {
    ADD: "[Question] Add",
    EDIT: "[Question] Edit",
    DELETE: "[Question] Delete",
    QUESTIONS_CHANGED: "[Question] Questions Changed"
};

export class QuestionEvent extends CustomEvent {
    constructor(eventName:string, question: Question) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { question }
        });
    }
}

export class QuestionAdd extends QuestionEvent {
    constructor(question: Question) {
        super(questionActions.ADD, question);        
    }
}

export class QuestionEdit extends QuestionEvent {
    constructor(question: Question) {
        super(questionActions.EDIT, question);
    }
}

export class QuestionDelete extends QuestionEvent {
    constructor(question: Question) {
        super(questionActions.DELETE, question);
    }
}

export class QuestionsChanged extends CustomEvent {
    constructor(questions: Array<Question>) {
        super(questionActions.QUESTIONS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { questions }
        });
    }
}
