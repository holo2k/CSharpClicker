const threshold = 10;
let maxHealth = 100;
let seconds = 0;
let clicks = 0;
const hitSound = new Audio('../hit.mp3');
const currentScoreElement = document.getElementById("current_score");
const recordScoreElement = document.getElementById("record_score");
const profitPerClickElement = document.getElementById("profit_per_click");
const profitPerSecondElement = document.getElementById("profit_per_second");
const orcImage = document.getElementById("orc-image");
const orcHealthBar = document.getElementById("orc-health-bar");
const healthBarText = document.getElementById("health-bar-text");
let orcHealth = Number(100);
let currentScore = parseInt(currentScoreElement.innerText.trim()) || 0;
let recordScore = parseInt(recordScoreElement.innerText.trim()) || 0;
let profitPerSecond = parseInt(profitPerSecondElement.innerText.trim()) || 0;
let profitPerClick = parseInt(profitPerClickElement.innerText.trim()) || 0;
const orcImages = [
    "../ORC_1.png",
    "../ORC_2.png",
    "../ORC_3.png",
    "../ORC_4.png",
    "../GREMLIN.png",
    "../DRAGON.png",
    "../SCELETON.png"
];



$(document).ready(function () {
    const clickitem = document.getElementById("clickitem");
    hitSound.volume = 0.2;
    clickitem.onclick = click;
    setInterval(addSecond, 1000)

    const boostButtons = document.getElementsByClassName("boost-button");

    toggleBoostsAvailability();

    for (let i = 0; i < boostButtons.length; i++) {
        const boostButton = boostButtons[i];

        boostButton.onclick = () => boostButtonClick(boostButton);
    }

    
})

function boostButtonClick(boostButton) {
    if (clicks > 0 || seconds > 0) {
        addPointsToScore();
    }
    buyBoost(boostButton);
}



function buyBoost(boostButton) {
    const boostIdElement = boostButton.getElementsByClassName("boost-id")[0];
    const boostId = boostIdElement.innerText;

    $.ajax({
        url: '/boost/buy',
        method: 'post',
        dataType: 'json',
        data: { boostId: boostId },
        success: (response) => onBuyBoostSuccess(response, boostButton),
    });
}

function onBuyBoostSuccess(response, boostButton) {
    const score = response["score"];

    const boostPriceElement = boostButton.getElementsByClassName("boost-price")[0];
    const boostQuantityElement = boostButton.getElementsByClassName("boost-quantity")[0];

    const boostPrice = Number(response["price"]);
    const boostQuantity = Number(response["quantity"]);

    boostPriceElement.innerText = boostPrice;
    boostQuantityElement.innerText = boostQuantity;

    updateScoreFromApi(score);
}

function addSecond() {
    seconds++;

    if (seconds >= threshold) {
        addPointsToScore();
    }

    if (seconds > 0) {
        addPointsFromSecond();
    }
}

function click() {
    clicks++;
    playHitSound();
    if (clicks >= threshold) {
        addPointsToScore();
    }

    if (clicks > 0) {
        addPointsFromClick();
    }
}

function playHitSound() {
    hitSound.currentTime = 0; 
    hitSound.play();
}

function updateScoreFromApi(scoreData) {
    currentScore = Number(scoreData["currentScore"]);
    recordScore = Number(scoreData["recordScore"]);
    profitPerClick = Number(scoreData["profitPerClick"]);
    profitPerSecond = Number(scoreData["profitPerSecond"]);

    updateUiScore();
}

function updateUiScore() {
    currentScoreElement.innerText = currentScore;
    recordScoreElement.innerText = recordScore;
    profitPerClickElement.innerText = profitPerClick;
    profitPerSecondElement.innerText = profitPerSecond;

    if (orcHealth <= 0) {
        orcHealth = (profitPerClick + profitPerSecond) * 10;
        maxHealth = (profitPerClick + profitPerSecond) * 10;
        orcImage.src = getRandomImage(orcImages);
    }

    const healthPercentage = Math.max(0, (orcHealth / maxHealth) * 100);
    orcHealthBar.style.width = `${healthPercentage}%`;
    healthBarText.textContent = `${orcHealth} / ${maxHealth}`;

    if (healthPercentage > 50) {
        orcHealthBar.style.backgroundColor = "#4caf50"; // Зелёный
    } else if (healthPercentage > 20) {
        orcHealthBar.style.backgroundColor = "#ffc107"; // Жёлтый
    } else {
        orcHealthBar.style.backgroundColor = "#f44336"; // Красный
    }

    toggleBoostsAvailability();
}

function getRandomImage(images) {
    const randomIndex = Math.floor(Math.random() * images.length);
    return images[randomIndex];
}


function addPointsFromClick() {
    currentScore += profitPerClick;
    recordScore += profitPerClick;
    orcHealth -= profitPerClick;

    updateUiScore();
}

function addPointsFromSecond() {
    currentScore += profitPerSecond;
    recordScore += profitPerSecond;
    orcHealth -= profitPerSecond;

    updateUiScore();
}

function addPointsToScore() {
    $.ajax({
        url: '/score',
        method: 'post',
        dataType: 'json',
        data: { clicks: clicks, seconds: seconds },
        success: (response) => onAddPointsSuccess(response),
    });
}

function onAddPointsSuccess(response) {
    seconds = 0;
    clicks = 0;

    updateScoreFromApi(response);
}

function toggleBoostsAvailability() {
    const boostButtons = document.getElementsByClassName("boost-button");

    for (let i = 0; i < boostButtons.length; i++) {
        const boostButton = boostButtons[i];

        const boostPriceElement = boostButton.getElementsByClassName("boost-price")[0];
        const boostPrice = Number(boostPriceElement.innerText);

        if (boostPrice > currentScore) {
            boostButton.disabled = true;
            continue;
        }

        boostButton.disabled = false;
    }
}