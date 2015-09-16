var driveApp = angular.module('driveApp', []);

driveApp.controller('DriveCtrl', function ($scope, $http) {

    var getFiles = function (path) {
        return $http.get('/api/drive/directory', { params: { path: path } })
            .success(function (data) {
                $scope.currentDirectory.currentPath = data.CurrentPath;
                $scope.currentDirectory.folders = data.Folders;
                $scope.currentDirectory.files = data.Files;
            });
    }

    var getFilesInfo = function (path) {
        return $http.get('api/drive/countFiles', { params: { path: path } })
            .success(function (data) {
                $scope.currentDirectory.less10mb = data.CountFilesLessThat_10mb;
                $scope.currentDirectory.between10_50mb = data.CountFiles_10_50mb;
                $scope.currentDirectory.more100mb = data.CountFilesMoreThat_100mb;
            });
    }

    $scope.currentDirectory = {
        currentPath: '',
        folders: [],
        files: [],
        less10mb: 0,
        between10_50mb: 0,
        more100mb: 0,

        getItems: function (folder) {
            if (folder == '' || folder == undefined){
                
            }
            var path = (this.currentPath == '') ? folder : this.currentPath + "\\" + folder;

            // Get folders and files in current directory.
            getFiles(path);

            // Get count files in current directory.
            getFilesInfo(path);
        },

        init: function () {
            this.getItems("\\");
        }

    };
});

