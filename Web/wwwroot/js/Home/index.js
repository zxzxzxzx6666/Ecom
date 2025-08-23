function AddItemToBasket(button) {
    // 找出與這個按鈕在同一個 form 內的四個 hidden 欄位的值
    var $form = $(button).closest('form');
    var data = {
        id: $form.find('input[name="id"]').val(),
        name: $form.find('input[name="name"]').val(),
        pictureUri: $form.find('input[name="pictureUri"]').val(),
        price: $form.find('input[name="price"]').val()
    };

    $.ajax({
        url: '/Basket/AddItemToBasket',
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success) {
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
