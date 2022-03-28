$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    var actions = $("table td:last-child").html();
    paintTable();

    $("#wordsTable td:nth-child(2)").each(function () {
        if (parseInt($(this).text()) < 0) {
            $(this).parent("tr").css("background-color", "#ffe0e0");
        }
        if (parseInt($(this).text()) > 0) {
            $(this).parent("tr").css("background-color", "#d6ffd9");
        }
    });

    // Append table with add row form on add new button click
    $(".add-new").click(function () {
        $(this).attr("disabled", "disabled");
        var index = $("table tbody tr:last-child").index();
        var row = '<tr>' +
            '<td><input type="text" class="form-control" id="wordDesc"></td>' +
            '<td><input type="text" class="form-control" id="wordSentimentScore"></td>' +
            '<td>' + actions + '</td>' +
            '</tr>';
        $("table").append(row);
        $("table tbody tr").eq(index + 1).find(".add, .edit").toggle();
        $('[data-toggle="tooltip"]').tooltip();
    });

    // Add row on add button click
    $(document).on("click", ".add", function () {
        var empty = false;
        var isValid = true;

        var input = $(this).parents("tr").find('input[type="text"]');

        input.each(function () {
            if (!$(this).val()) {
                $(this).addClass("error");
                empty = true;
            } else {
                $(this).removeClass("error");
            }
        });

        $(this).parents("tr").find(".error").first().focus();

        var isValid = validateInput(input);
        if (isValid == 0)
        {
            return;
        }

        if (!empty) {
            input.each(function () {
                $(this).parent("td").html($(this).val());
            });
            $(this).parents("tr").find(".add, .edit").toggle();

            var $row = $(this).closest('tr');
            var wordId = $row.find("#wordDesc").attr('data-attribute');
            var word = $row.find("td:eq(0)").text();
            var sentimentScore = $row.find("td:eq(1)").text();

            if (isValid) {
                $.ajax(
                    {
                        type: 'POST',
                        dataType: 'JSON',
                        url: '/Home/AddEditWord',
                        data: { wordId: wordId, wordDesc: word, sentimentScore: sentimentScore },
                        success: function (data) {
                            if (!wordId > 0) {
                                $("#wordsTable").find("tr").last().find("td").first().attr('data-attribute', data);
                                $("#wordsTable").find("tr").last().find("td").first().attr('Id', "wordDesc");
                            }
                        }
                    });
            }

            $(".add-new").removeAttr("disabled");
            paintTable();
        }
    });

    function validateInput(input)
    {
        if (isNaN(input[1].value)) {
            $('#errorMessage').text('Sentiment score must be a number.');
            $('#errorMessage').css("display", "block");
            $('#errorMessage').css("color", "red");
            isValid = false;
            return 0;
        }
        else { $('#errorMessage').css("display", "none"); }

        if (parseFloat(input[1].value) < -1 || parseFloat(input[1].value) > 1) {
            $('#errorMessage').text('Sentiment score must be a value between -1 and 1');
            $('#errorMessage').css("display", "block");
            $('#errorMessage').css("color", "red");
            isValid = false;
            return 0;
        }
        else { $('#errorMessage').css("display", "none"); }

        var colArray = $('#wordsTable td:nth-child(1)').map(function () {
            return $(this).text();
        }).get();

        var arrayContainsWord = (colArray.indexOf(input[0].value) > -1);

        if (arrayContainsWord) {
            if (input[0].value !== '') {
                $('#errorMessage').text('Word already exists.');
                $('#errorMessage').css("display", "block");
                $('#errorMessage').css("color", "red");
                isValid = false;
                return 0;
            }
        }
        else { $('#errorMessage').css("display", "none"); }
    }

    $("#ddlFilters").change(function () {
        var value = $(this).val();

        window.location.href = '/Home/FilterWords?filterId=' + value;
    });

    function paintTable() {
        $("#wordsTable td:nth-child(2)").each(function () {
            if (parseFloat($(this).text()) < 0) {
                $(this).parent("tr").css("background-color", "#ffe0e0");
            }
            if (parseFloat($(this).text()) > 0) {
                $(this).parent("tr").css("background-color", "#d6ffd9");
            }
        });
    }

    // Edit row on edit button click
    $(document).on("click", ".edit", function () {
        $(this).parents("tr").find("td:not(:last-child)").each(function () {
            $(this).html('<input type="text" class="form-control" value="' + $(this).text() + '">');
        });
        $(this).parents("tr").find(".add, .edit").toggle();
        $(".add-new").attr("disabled", "disabled");
    });

    // Delete row on delete button click
    $(document).on("click", ".delete", function () {
        var $row = $(this).closest('tr');
        var wordId = $row.find("#wordDesc").attr('data-attribute');
        $.ajax(
            {
                type: 'POST',
                dataType: 'JSON',
                url: '/Home/DeleteWord',
                data: { wordId: wordId }
            });

        $(this).parents("tr").remove();
        $(".add-new").removeAttr("disabled");
    });
});