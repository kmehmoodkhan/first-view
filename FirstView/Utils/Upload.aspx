<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="FirstView.Utils.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />    
    <link href="../Content/dropzone.css" rel="stylesheet" />
    <script src="../Scripts/dropzone.js" type="text/javascript"></script>
    <script type="text/javascript">
        Dropzone.options.myAwesomeDropzone = {
         paramName: "file", // The name that will be used to transfer the file
         maxFilesize: 15, // MB
         acceptedFiles: "image/*",         
         maxFiles: 1,
         maxfilesexceeded: function (file) {
             this.removeAllFiles();
             this.addFile(file);
         }
     }
    </script>
</head>
<body>
     <form id="myAwesomeDropzone" runat="server" class="dropzone">
            <div>
                <div class="fallback">
                    <input name="file" type="file" />
                </div>
                <div class="dz-message needsclick">
                    Drop image files here or click to upload.
                </div>
            </div>
        </form>
</body>
</html>
