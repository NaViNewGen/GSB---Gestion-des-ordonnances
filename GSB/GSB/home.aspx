<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="GSB.home" %>

<!doctype html>
<html lang="fr">

<head>

    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
    <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css'>
    <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css'>
    <link href="https://fonts.googleapis.com/css2?family=PT+Sans&display=swap" rel="stylesheet">
    <link rel="icon" href="{{ url_for('static',filename='user.png')}}">
</head>
<title>Galaxy Swiss Bourdin</title>


</head>

<body
    style="font-family: 'PT Sans', sans-serif;background-image: url(home.jpg);background-size: cover;">
    <nav class='navbar navbar-expand-lg navbar-dark bg-dark'>

        <div class='container-fluid' id='navbarNav'>
            <ul class='nav navbar-nav navbar-brand'>
                <li class='nav-item'>
                    <a class='navbar-brand' href='/'>Accueil</a>
                </li>
            </ul>
            <ul class='nav navbar-nav ml-auto'>
                <li class='nav-item active ml-4'>
                    <a class='nav-link' href='medecin/login_medecin.aspx'>Médecin</a>
                </li>
                <li class='nav-item active ml-4'>
                    <a class='nav-link' href='pharmacien/login_pharmacien.aspx'>Pharmacien</a>
                </li>
                <li class='nav-item active ml-4'>
                    <a class='nav-link' href='patient/login_patient.aspx'>Patient</a>
                </li>
                <li class='nav-item active ml-4'>
                    <a class='nav-link' href='administrateur/login_admin.aspx'>Administrateur</a>
                </li>
            </ul>
        </div>
    </nav>
    <div class="container" style="color: white;text-shadow: 2px 2px 12px #000000;">

            <div class="info-area" style="background-color: rgba(11, 88, 109, 0.2); padding: 20px;">
                <h1>Galaxy Swiss Bourdin</h1>
                
                <br />
                <br />
                <br />
                <h2>A propos de Galaxy Swiss Bourdin</h2>
                

                <p>Le laboratoire GSB (Galaxy Swiss Bourdin) est une industrie pharmaceutique spécialisée dans 
                    la recherche et production de nouvelles molécules et médicaments révolutionnaires.
                    L'industrie pharmaceutique est un secteur très lucratif dans lequel le mouvement de fusion 
                    d'acquisition est très fort. Notre laboratoire est lui issu de la fusion du géant américain 
                    Galaxy et le conglomérat européen Swiss Bourdin.
                </p>
            </div>

        <hr style="border: 5px solid black;border-radius: 5px;" />
        <footer>
            <p>&copy; 2023 - Galaxy Swiss Bourdin</p>
        </footer>
    </div>


    <script src='https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js'></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js'></script>
    <script src="{{ url_for('static',filename='script.js')}}"></script>
</body>

</html>
