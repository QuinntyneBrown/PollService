import { QuestionAdd, QuestionDelete, QuestionEdit, QuestionsChanged, questionActions } from "./question.actions";
import { Question } from "./question.model";

const template = require("./question-master-detail-embed.component.html");
const styles = require("./question-master-detail-embed.component.scss");

export class QuestionMasterDetailEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onQuestionAdd = this.onQuestionAdd.bind(this);
        this.onQuestionEdit = this.onQuestionEdit.bind(this);
        this.onQuestionDelete = this.onQuestionDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "questions"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.questionListElement.setAttribute("questions", JSON.stringify(this.questions));
    }

    private _setEventListeners() {
        this.addEventListener(questionActions.ADD, this.onQuestionAdd);
        this.addEventListener(questionActions.EDIT, this.onQuestionEdit);
        this.addEventListener(questionActions.DELETE, this.onQuestionDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(questionActions.ADD, this.onQuestionAdd);
        this.removeEventListener(questionActions.EDIT, this.onQuestionEdit);
        this.removeEventListener(questionActions.DELETE, this.onQuestionDelete);
    }

    public onQuestionAdd(e) {

        const index = this.questions.findIndex(o => o.id == e.detail.question.id);
        const indexBaseOnUniqueIdentifier = this.questions.findIndex(o => o.name == e.detail.question.name);

        if (index > -1 && e.detail.question.id != null) {
            this.questions[index] = e.detail.question;
        } else if (indexBaseOnUniqueIdentifier > -1) {
            for (var i = 0; i < this.questions.length; ++i) {
                if (this.questions[i].name == e.detail.question.name)
                    this.questions[i] = e.detail.question;
            }
        } else {
            this.questions.push(e.detail.question);
        }
        
        this.questionListElement.setAttribute("questions", JSON.stringify(this.questions));
        this.questionEditElement.setAttribute("question", JSON.stringify(new Question()));
        this.dispatchEvent(new QuestionsChanged(this.questions));
    }

    public onQuestionEdit(e) {
        this.questionEditElement.setAttribute("question", JSON.stringify(e.detail.question));
    }

    public onQuestionDelete(e) {
        if (e.detail.question.Id != null && e.detail.question.Id != undefined) {
            this.questions.splice(this.questions.findIndex(o => o.id == e.detail.optionId), 1);
        } else {
            for (var i = 0; i < this.questions.length; ++i) {
                if (this.questions[i].name == e.detail.question.name)
                    this.questions.splice(i, 1);
            }
        }

        this.questionListElement.setAttribute("questions", JSON.stringify(this.questions));
        this.questionEditElement.setAttribute("question", JSON.stringify(new Question()));
        this.dispatchEvent(new QuestionsChanged(this.questions));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "questions":
                this.questions = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Question> { return this.questions; }

    private questions: Array<Question> = [];
    public question: Question = <Question>{};
    public get questionEditElement(): HTMLElement { return this.querySelector("ce-question-edit-embed") as HTMLElement; }
    public get questionListElement(): HTMLElement { return this.querySelector("ce-question-list-embed") as HTMLElement; }
}

customElements.define(`ce-question-master-detail-embed`,QuestionMasterDetailEmbedComponent);
