// Write your Javascript code.


    
function makeTableScroll() {
    // Constant retrieved from server-side via JSP
    var maxRows = 12;

    var table = document.getElementById('editable');
    var wrapper = table.parentNode;
    var rowsInTable = table.rows.length;
    try {
        var border = getComputedStyle(table.rows[0].cells[0], '').getPropertyValue('border-top-width');
        border = border.replace('px', '') * 1;
    } catch (e) {
        var border = table.rows[0].cells[0].currentStyle.borderWidth;
        border = (border.replace('px', '') * 1) / 2;
    }
    var height = 0;
    if (rowsInTable > maxRows) {
        for (var i = 0; i < maxRows; i++) {
            height += table.rows[i].clientHeight + border;
        }
        wrapper.style.height = height + "px";
    }
}

function maketableScroll2() {
   
    $('table.table').each(function() {
        var currentPage = 0;
        var numPerPage = 10;
        var $table = $(this);
        $table.bind('repaginate', function() {
            $table.find('.trcont').hide().slice(currentPage * numPerPage, (currentPage + 1) * numPerPage).show();
        });
        $table.trigger('repaginate');
        var numRows = $table.find('.trcont').length;
        var numPages = Math.ceil(numRows / numPerPage);
        var $pager = $('<div class="pager"></div>');
        for (var page = 0; page < numPages; page++) {
            $('<span class="page-number"></span>').text(page + 1).bind('click', {
                newPage: page
            }, function(event) {
                currentPage = event.data['newPage'];
                $table.trigger('repaginate');
                $(this).addClass('active').siblings().removeClass('active');
            }).appendTo($pager).addClass('clickable');
        }
        $pager.insertBefore($table).find('span.page-number:first').addClass('active');
    });
}
function deleteRow(id)
{
            
}

function saveTableBalcoes()
    {

        
    }

function editarDados()
{


}

