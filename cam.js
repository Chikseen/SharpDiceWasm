import { dotnet } from "./dotnet.js";

//////////////////////// BEGIN DOTNET SETUP
const { getAssemblyExports, getConfig } = await dotnet.withDiagnosticTracing(false).withApplicationArgumentsFromQuery().create();
const config = getConfig();
const exports = await getAssemblyExports(config.mainAssemblyName);
await dotnet.run();

//////////////////////// END DOTNET SETUP

document.getElementById("saveBtn").addEventListener("click", save);

const videoelement = document.getElementById("videoelement");
const streamContraints = {
	audio: false,
	video: { facingMode: "user" },
};
const mainCanvas = document.getElementById("mainCanvas");
const mainctx = mainCanvas.getContext("2d");
const fps = 144;
var canvasInterval = null;

navigator.mediaDevices.getUserMedia(streamContraints).then(gotStream);

function gotStream(stream) {
	videoelement.srcObject = stream;
	videoelement.play();
}

function drawImage(video) {
	mainctx.drawImage(video, 0, 0, mainCanvas.width, mainCanvas.height);
}
canvasInterval = window.setInterval(() => {
	drawImage(videoelement);
}, 1000 / fps);

///////////////////////////////
function save() {
	const timeToCompleteStart = performance.now();
	const canvas = document.getElementById("mainCanvas");
	const canvasWidth = canvas.getBoundingClientRect().width.toFixed(0) * 1;
	const canvasHeight = canvas.getBoundingClientRect().height;

	const ctx = canvas.getContext("2d");
	const image = ctx.getImageData(0, 0, canvasWidth, canvasHeight);
	const pixel = image.data;
	const byteArray = Array.prototype.slice.call(pixel);

	const data = exports.Main.LookAtMe(byteArray, canvasWidth);

	/// debug screen
	const overlayCan = document.getElementById("overlayCanvas");
	const overlayCtx = overlayCan.getContext("2d");

	const pixels = new Uint8ClampedArray(data);
	const imageData = new ImageData(pixels, canvasWidth, canvasHeight);
	overlayCtx.putImageData(imageData, 0, 0);

	// FPS DISPLAY
	const timeToCompleteEnd = performance.now();
	document.getElementById("fps").innerHTML = (timeToCompleteEnd - timeToCompleteStart).toFixed(0) + " ms";

	setTimeout(save, 0);
}
