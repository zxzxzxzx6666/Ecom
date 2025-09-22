function Checkout(button) {
    var data = { Id : parseInt($('.esh-basket-id').text())};

    $.ajax({
        url: '/Basket/Payment',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data), // 序列化
        success: function (response) {
            if (response.success) {
                // redirect to home page
                window.location.href = '/Home/Index';
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

