import { Routes } from '@angular/router';
import { LoginComponent } from './Auth/login/login.component';
import { HomeComponent } from './Home/home/home.component';
import { AboutComponent } from './About/about/about.component';
import { ContactComponent } from './Contact/contact/contact.component';
import { PostComponent } from './Post/post/post.component';
import { RegisterComponent } from './Auth/register/register.component';
import { DetailPostComponent } from './Post/post/detail-post/detail-post.component';
import { AuthGuard } from './Auth/auth.guard';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },

    {
        path: 'about',
        component: AboutComponent
    },
    {
        path: 'home',
        canActivate: [AuthGuard],
        component: HomeComponent
    },
    {
        path: 'contact',
        component: ContactComponent
    },
    {
        path: 'post',
        component: PostComponent
    },
    {
        path: 'post/:id',
        component: DetailPostComponent
    },
    
];
