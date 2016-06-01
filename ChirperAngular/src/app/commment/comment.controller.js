(function() {
    'use strict';

    angular
        .module('app')
        .controller('CommentController', CommentController);

    CommentController.$inject = ['$http','CommentFactory','toastr'];

    /* @ngInject */
    function CommentController($http,CommentFactory,toastr) {
        var cm = this;
        // cm.title = 'CommentController';

        function activate() {
        	CommentFactory.getComments()
        		.then(function(response){
        			cm.comments = response.data
        			console.log(cm.comments)
        		});
        };

        cm.addComment = function(chirpId, newComment){
        	CommentFactory.addComment(chirpId, newComment)
        		.then(function(response){
        			cm.newComment = response.data;
        			//cm.comments.shift(cm.newComment);
                    cm.comment = null;
        		});
                CommentFactory.getComments();
        };

        activate();
        // cm.comment = "test";
    }
})();