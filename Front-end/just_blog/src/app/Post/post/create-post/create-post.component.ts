import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { QuillEditorComponent } from 'ngx-quill';
import { CategoryService } from '../category.service';
import { PostService } from '../post.service';
@Component({
  selector: 'app-create-post',
  standalone: true, imports: [MatIconModule, QuillEditorComponent, MatFormFieldModule, MatSelectModule, CommonModule, MatInputModule, FormsModule],
  templateUrl: './create-post.component.html',
  styleUrl: './create-post.component.scss'
})
export class CreatePostComponent implements OnInit {
  private dialogRef = inject(MatDialogRef<CreatePostComponent>)
  private cate_services = inject(CategoryService)
  private cdr = inject(ChangeDetectorRef)
  private post_services=inject(PostService)
  content!: string;
  description!:string;
  title!:string;
  listCate: any[] = [];
  foods: any[] = [
    { value: 'steak-0', viewValue: 'Steak' },
    { value: 'pizza-1', viewValue: 'Pizza' },
    { value: 'tacos-2', viewValue: 'Tacos' },
  ];
  ngOnInit(): void {
    this.getCate()
  }
  getCate() {
    this.cate_services.GetCategory().subscribe(res => {
    this.listCate=res;
    this.cdr.detectChanges();
    }
    )
  }
  PublishPost()
  {
    let payload={
      "title": this.title,
      "description": this.description,
      "content": this.content,
      "categoryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    }
    this.post_services.CreatedPost(payload).subscribe(res=>
    {
      this.dialogRef.close(res)
    }
    )
  }

  Close() {
    this.dialogRef.close()
  }
}
