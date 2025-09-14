function UpdateBasket(button) {
    // 找出與這個按鈕在同一個 form 內的四個 hidden 欄位的值
    var $form = $(button).closest('form');

    // 找出所有 basket-item-UpdateOrChange
    var data = [];
    $form.find('section[name="basket-item-UpdateOrChange"]').each(function () {
        var id = $(this).find('.esh-basket-item-id').val();
        var quantity = $(this).find('.esh-basket-item-quentity').val();

        data.push({
            Id: parseInt(id), 
            Quantity: parseInt(quantity) 
        });
    });

    $.ajax({
        url: '/Basket/UpdateBasket',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data), // 序列化
        success: function (response) {
            if (response.success) {
                // refresh total cost
                location.reload();
                alert(response.message);
            } else {
                alert('操作失敗');
            }
        },
        error: function () {
            alert('發生錯誤');
        }
    });

    // 阻止表單送出
    return false;
}

