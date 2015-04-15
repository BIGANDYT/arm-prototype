<?php
    $hostname = "sitecore";    
    
    function uploadImage() {
        global $hostname;

        $ch = curl_init();
        $filePath = $_FILES['file']['tmp_name'];
        $fileName = $_FILES['file']['name'];
        $fileType = $_FILES['file']['type'];
        $cfile = new CURLFile($filePath, $fileType, $fileName);
        $data = array('name' => $_POST['name'], 'file' => $cfile);  
        curl_setopt($ch, CURLOPT_URL, "http://{$hostname}/-/item/v1/sitecore/media%20library/Images/ARM/FromForm");
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
        curl_setopt($ch, CURLOPT_POST, 1);
        curl_setopt($ch, CURLOPT_POSTFIELDS, $data);
        curl_setopt($ch, CURLOPT_HTTPHEADER, array(
            'X-Scitemwebapi-Username: sitecore\admin',
            'X-Scitemwebapi-Password: b'
        ));
        $response = curl_exec($ch);
    
        curl_close($ch);
    
        $json = json_decode($response);
        if ($json->statusCode != "200") {
            var_dump($response);
            die("Upload failed");
        }
    
        if ($json->result->totalCount != 1) {
            die("File not created");
        }
    
        $itemID = $json->result->items[0]->ID;    
        return $itemID;    
    }

    function tagImage($itemID) {
        global $hostname;

        $ch = curl_init();
        $data = array('tagIDs' => implode("|", $_POST['options']));
        curl_setopt($ch, CURLOPT_URL, "http://{$hostname}/api/taxonomy/tagItem/{$itemID}");
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
        curl_setopt($ch, CURLOPT_POST, 1);
        curl_setopt($ch, CURLOPT_POSTFIELDS, $data);
        curl_setopt($ch, CURLOPT_HTTPHEADER, array(
            'X-Scitemwebapi-Username: sitecore\admin',
            'X-Scitemwebapi-Password: b',
        ));
        $response = curl_exec($ch);
    }
    
    if (isset($_POST["submit"])) {
        $itemID = uploadImage();
        tagImage($itemID);
        header("Location: /?done=1");
        die();
    }
?>
<!DOCTYPE html>
<html>
    <head>
        <title>ARM PHP Asset Uploader</title>
        <script>
            var hostname = '<?php echo $hostname?>';
        </script>
        <style type="text/css">
            #taxonomy {
                margin-top: 10px;
            }
            #taxonomy .category {
                margin-bottom: 10px;
            }
            #taxonomy .category label {
                display: block; 
                cursor: pointer;
            }            
            .category-title {
                font-weight: 700;
            }
        </style>
    </head>
    <body>
        <div>
            <img src="/arm_logo.gif" alt="ARM Logo" />
        </div>

        <h1>ARM PHP Asset Uploader</h1>

        <?php if(isset($_GET['done'])) {?>
        <p>File uploaded</p>
        <?php } ?>

        <form action="?" method="post" enctype="multipart/form-data">
            <label>
                Name: <input type="text" name="name" value="" /><br /><br />
            </label>
            <label>
                File: <input type="file" name="file" /><br />
            </label>

            <div id="taxonomy"></div>

            <input type="submit" value="Upload file" />
            <input type="hidden" name="submit" value="1" />
        </form>

        <script src="/jquery-1.11.2.min.js"></script>
        <script>
            (function ($) {
                $(document).ready(function () {
                    $.ajax({
                        url: "http://" + hostname + "/api/taxonomy/categories"
                    }).done(function (r) {
                        for (var x in r) {
                            (function(categoryID, categoryName) {
                                var category = $('<div class="category"></div>').attr('data-category-id', categoryID);
                                category.append($('<div class="category-title"></div>').html(categoryName));
            
                                $.ajax({
                                    url: "http://" + hostname + "/api/taxonomy/categories/" + categoryID
                                }).done(function(r2) {
                                    for(var y in r2) {
                                        var optionID = y;
                                        var optionName = r2[y];
            
                                        var label = $('<label></label>');
                                        var checkbox = $('<input type="checkbox" name="options[]" />').attr('value', optionID);
                                        label.append(checkbox);
                                        label.append($('<span></span>').html(optionName));
                                        category.append(label);
                                    }
                                });                
            
                                $('#taxonomy').append(category);
                            })(x, r[x]);
                        }
                    });
                });
            })(jQuery);
        </script>
    </body>
</html>