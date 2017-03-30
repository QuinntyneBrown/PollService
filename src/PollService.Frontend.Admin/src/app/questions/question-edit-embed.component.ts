import { Question } from "./question.model";
import { EditorComponent } from "../shared";
import { QuestionDelete, QuestionEdit, QuestionAdd } from "./question.actions";
import { createHTMLOption } from "../utilities";

const template = require("./question-edit-embed.component.html");
const styles = require("./question-edit-embed.component.scss");

export class QuestionEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "question",
            "question-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }

    public onCreate() {
        this.dispatchEvent(new QuestionEdit(new Question()));
    }

    private async _bind() {
        this.bodyEditor = new EditorComponent(this._bodyElement);
        this.descriptionEditor = new EditorComponent(this._descriptionElement);

        this._selectElement.appendChild(createHTMLOption({ textContent: "- SELECT TYPE -", value: "" }));
        this._selectElement.appendChild(createHTMLOption({ textContent: "Default", value: "0" }));
        this._selectElement.appendChild(createHTMLOption({ textContent: "Single Line", value: "1" }));
        this._selectElement.appendChild(createHTMLOption({ textContent: "HTML", value: "2" }));
        this._selectElement.appendChild(createHTMLOption({ textContent: "Date", value: "3" }));
        this._selectElement.appendChild(createHTMLOption({ textContent: "Select List", value: "4" }));        

        
        this._titleElement.textContent = this.question ? "Edit Question": "Create Question";

        if (this.question) {                            
            this._nameInputElement.value = this.question.name; 
            this._orderIndexInputElement.value = this.question.orderIndex;
            this.bodyEditor.setHTML(this.question.body); 
            this.descriptionEditor.setHTML(this.question.description);
            this._selectElement.value = <any>this.question.questionType;
        } else {
            this._deleteButtonElement.style.display = "none";
            this._nameInputElement.value = "0-0";
            this._orderIndexInputElement.value = "0";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._createElement.addEventListener("click", this.onCreate);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._createElement.removeEventListener("click", this.onCreate);
    }

    public onSave() {
        const question = {
            id: this.question != null ? this.question.id : null,
            name: this._nameInputElement.value,
            orderIndex: this._orderIndexInputElement.value,
            body: this.bodyEditor.text,
            description: this.descriptionEditor.text
        } as Question;
        
        this.dispatchEvent(new QuestionAdd(question));            
    }

    public onDelete() {        
        const question = {
            id: this.question != null ? this.question.id : null,
            name: this._nameInputElement.value
        } as Question;

        this.dispatchEvent(new QuestionDelete(question));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "question-id":
                this.questionId = newValue;
                break;
            case "question":
                this.question = JSON.parse(newValue);
                if (this.parentNode) {
                    this.questionId = this.question.id;
                    this._selectElement.value = <any>this.question.questionType;
                    this._nameInputElement.value = this.question.name != undefined ? this.question.name : "";
                    this._orderIndexInputElement.value = this.question.orderIndex != undefined ? this.question.orderIndex : "";
                    this.bodyEditor.setHTML(this.question.body != undefined ? this.question.body : ""); 
                    this.descriptionEditor.setHTML(this.question.description != undefined ? this.question.description : "");
                    this._titleElement.textContent = this.questionId ? "Edit Question" : "Create Question";
                }
                break;
        }           
    }

    public questionId: any;
    public question: Question;
    public bodyEditor: EditorComponent;
    public descriptionEditor: EditorComponent;

    private get _selectElement(): HTMLSelectElement { return this.querySelector(".question-type") as HTMLSelectElement; }
    private get _createElement(): HTMLElement { return this.querySelector(".question-create") as HTMLElement; }
    private get _bodyElement(): HTMLElement { return this.querySelector(".question-body") as HTMLElement; }
    private get _descriptionElement(): HTMLElement { return this.querySelector(".question-description") as HTMLElement; }
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".question-name") as HTMLInputElement; }
    private get _orderIndexInputElement(): HTMLInputElement { return this.querySelector(".question-order-index") as HTMLInputElement; }
    
}

customElements.define(`ce-question-edit-embed`,QuestionEditEmbedComponent);
