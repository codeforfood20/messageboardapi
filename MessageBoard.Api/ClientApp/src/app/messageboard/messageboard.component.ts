import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from "@angular/forms";

@Component({
  selector: 'mb-messageboard',
  templateUrl: './messageboard.component.html',
})
export class MessageBoardComponent implements OnInit {
  baseUrl: string = "http://localhost:50849/api/messageboard/messages";

  http: HttpClient;

  messagesResponse: GetMessagesResponse = {
    messages: []
  };

  form: FormGroup;

  showReply: boolean = false;

  constructor(http: HttpClient, fb: FormBuilder) {
    this.form = fb.group({
      user: [''],
      message: ['']
    });

    this.http = http;
    this.getMessages();
  }

  ngOnInit() {
    console.log("estiing");
  }

  getMessages(): any {
    this.http.get<GetMessagesResponse>(this.baseUrl).subscribe(result => {
      this.messagesResponse = result;
    }, error => console.error(error));
  }

  reply(): void {
    this.showReply = true;
  }

  postReply(): void {
    console.log("Posting reply" + this.form.get('user').value);
    let request: SendMessageRequest = {
      user: this.form.get('user').value,
      message: this.form.get('message').value
    }
    
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };
   
    this.http.post(this.baseUrl, request).subscribe(result => {
      console.log("Back from send message...");
      this.getMessages();
      this.showReply = false;
    }, error => console.error(error));
  }

  cancelReply(): void {
    this.showReply = false;
  }
}

interface GetMessagesResponse {
  messages: Message[];
}

interface Message {
  user: string;
  content: string;
  created: Date;
}

interface SendMessageRequest {
  user: string;
  message: string;
}
