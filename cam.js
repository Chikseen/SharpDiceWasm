import { dotnet } from "./dotnet.js";

//////////////////////// BEGIN DOTNET SETUP
const { setModuleImports, getAssemblyExports, getConfig } = await dotnet.withDiagnosticTracing(false).withApplicationArgumentsFromQuery().create();

setModuleImports("t.js", {
	window: {
		location: {
			href: () => globalThis.window.location.href,
		},
	},
});

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
let framesPerSec = 0;
let started = new Date();

async function save() {
	const canvas = document.getElementById("mainCanvas");
	const canvasWidth = canvas.getBoundingClientRect().width;
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

	document.getElementById("counter").innerHTML = document.getElementById("counter").innerHTML * 1 + 1;
	framesPerSec++;
	framesPerSec = ((new Date() - started) / framesPerSec).toFixed(0);
	document.getElementById("fps").innerHTML = framesPerSec;
	setTimeout(save, 1);
}
