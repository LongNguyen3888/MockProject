import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { PostService } from '../post.service';

@Component({
  selector: 'app-detail-post',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './detail-post.component.html',
  styleUrl: './detail-post.component.scss'
})
export class DetailPostComponent implements OnInit {
  private post_services=inject(PostService)
  private route=inject(ActivatedRoute)
  private cdr=inject(ChangeDetectorRef)
  userInformation=localStorage.getItem("userInformation")
  idPost!:string;
  PostDetail:any
  ngOnInit(): void {
    this.route.params.subscribe(pr=>
    {
      console.log("prrrr",pr);
      
          this.idPost=pr["id"]
    this.GetPostDetail(pr["id"]);

    }
    )

  }
  GetPostDetail(id:string)
  {
    this.post_services.GetPostDetail(id).subscribe(res=>
    {
      this.PostDetail=res;
      this.cdr.detectChanges();
      
    }
    )
  }

}
