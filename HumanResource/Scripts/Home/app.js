var driveApp = angular.module('driveApp', [])
    .controller("DriveCtrl", function ($scope, $http) {
        $scope.currentDirectory = {
            currentPath: "",
            folders: [],
            files: [],
            // Get folders and files in directory.
            getItems: function (folder) {
                var path = (this.currentPath == "") ? folder : this.currentPath + "\\" + folder;
                return $http.get('/api/drive/directory', { params: { path: path } })
                    .success(function (data) {
                        this.currentPath = data.CurrentPath;
                        this.folders = data.Folders;
                        this.files = data.Files;
                        // Start calculate count files for current directory.
                        $scope.currentDirectory.getCountFiles();
                    });
            },
            // Get count files in directory and subdirectories. 
            getCountFiles: function () {
                return $http.get('api/drive/countFiles', { params: { path: this.currentPath } }).success(function (data) {
                    this.less10mb = data.CountFilesLessThat_10mb;
                    this.between10_50mb = data.CountFiles_10_50mb;
                    this.more100mb = data.CountFilesMoreThat_100mb;
                });
            }
        };
    });

