function ModalEndereco() {
    $('#modalEndereco').modal('toggle');

    $('#modalEndereco').on('hidden.bs.modal', function () {
        $('#myForm input').val('');
    });
};

function VerificarCep(cep) {

    const valorCep = cep;

    $.ajax({
        url: "/Home/ConsultarCEP/" + valorCep,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.errorMessage) {
                $('#errorMessage').removeAttr("hidden");
                $("#errorMessage").text("Erro ao consultar o CEP");
            }
            else {
                $('#errorMessage').attr("hidden", true);
                $('#txtCEP').val(data.cep);
                $('#txtEndereco').val(data.endereco);
                $('#txtCidade').val(data.cidade);
                $('#txtEstado').val(data.estado);

            }
        },
        error: function (response) {

        }
    });
};

function CadastrarPessoa() {

    let pessoa = {
        "Nome": $('#txtNome').val(),
        "Telefone": $('#txtTelefone').val(),
        "CPF": $('#txtCpf').val()

    };

    $.ajax({
        url: "/Home/CadastroPessoa/",
        type: "POST",
        data: JSON.stringify(pessoa),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            window.location.href = response.redirectToUrl;

        },
        error: function (response) {
            window.location.href = response.redirectToUrl;
        }
    });

}

function SalvarPessoa() {

    let pessoa = {
        "Id": $('#txtIdPessoa').val(),
        "Nome": $('#txtNome').val(),
        "Telefone": $('#txtTelefone').val(),
        "CPF": $('#txtCpf').val()
    };

    $.ajax({
        url: "/Home/EditarPessoa/",
        type: "POST",
        data: JSON.stringify(pessoa),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            window.location.href = response.redirectToUrl;

        },
        error: function (response) {
            window.location.href = response.redirectToUrl;
        }
    });

}

function EditarEndereco() {

    ModalEndereco();

    const parent = $(event.target).parents().closest('tr');
    const idEndereco = parent.find('.tdIdEndereco').text();
    const cep = parent.find('.tdCep').text();

    $('#txtIdEndereco').val(idEndereco);
    $('#txtCep').val(cep);

    VerificarCep(cep);

    $('#btnCadastrar').attr('hidden', true);
    $('#btnSalvar').attr('hidden', false);
};

function SalvarEndereco() {

    let pessoa = {
        "Id": $('#txtIdEndereco').val(),
        "PessoaId": $('#txtIdPessoa').val(),
        "Cep": $('#txtCep').val(),
        "Endereco": $('#txtEndereco').val(),
        "Cidade": $('#txtCidade').val(),
        "Estado": $('#txtEstado').val()

    };

    $.ajax({
        url: "/Home/EditarEndereco/",
        type: "POST",
        data: JSON.stringify(pessoa),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            window.location.href = response.redirectToUrl;
        },
        error: function (response) {
            window.location.href = response.redirectToUrl;
        }
    });
};

function Cancelar() {
    $('#btnCancelar').attr('hidden', true);
};

function CadastrarEndereco() {

    let pessoa = {
        "PessoaId": $('#txtIdPessoa').val(),
        "Cep": $('#txtCep').val(),
        "Endereco": $('#txtEndereco').val(),
        "Cidade": $('#txtCidade').val(),
        "Estado": $('#txtEstado').val()
    };

    $.ajax({
        url: "/Home/CadastrarEndereco/",
        type: "POST",
        data: JSON.stringify(pessoa),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            window.location.href = response.redirectToUrl;
        },
        error: function (response) {
            window.location.href = response.redirectToUrl;
        }
    });
};

function ExcluirEndereco(id) {
    $('#btnExcluir').attr('href', '/Home/ExcluirEndereco/' + id);
    $('#modalExcluir').modal('toggle');
}