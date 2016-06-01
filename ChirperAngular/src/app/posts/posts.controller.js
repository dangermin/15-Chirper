(function() {
    'use strict';

    angular
        .module('app')
        .controller('PostsController', PostsController);

    PostsController.$inject = ['$http', 'PostsFactory', 'toastr'];

    /* @ngInject */
    function PostsController($http, PostsFactory, toastr) {
        var Posts = this;

        function activate() {
            PostsFactory.getPosts()
                .then(function(response) {
                    // console.log(response)
                    Posts.posts = response.data;
                    console.log(Posts.posts) 

                    var comments =  response.data;
                    Posts.comObjs = [];

                    // $.each(comments, function(i){
                    //     var comment = comments[i];
                    //     $.each(comment.Comments, function(i){
                    //         var comObj = comment.Comments[i];
                    //         console.log(comObj.Text)
                    //         Posts.comObjs.push(comObj);
                    //         console.log(Posts.comObjs)
                    //     })
                    // });
                });

        };

        Posts.addPost = function (newPost){
            PostsFactory.addPost(newPost)
                .then(function(response){
                    Posts.newPost = response.data;
                    Posts.posts.push(Posts.newPost);
                    Posts.newPost = null;
                });
        };

         Posts.likePost = function(Post){
            var likeCount = Posts.likeCount;

            PostsFactory.likePost(Post)
                .then(function(response){
                    Posts.likeCount += 1;
                });
        };

        Posts.deletePost = function(post){
            PostsFactory.deletePost(post)
                .then(function(response){
                    post.splice(post);
                });
        };

        activate();
    }
})();
