const express = require('express');
const path = require('path');
const fs = require('fs');

const app = express();
const PORT = process.env.PORT || 3000;

// Static files middleware
app.use(express.static(path.join(__dirname)));

// Ensure downloads directory exists
const downloadsDir = path.join(__dirname, 'downloads');
if (!fs.existsSync(downloadsDir)) {
    fs.mkdirSync(downloadsDir, { recursive: true });
}

// APK Download Route with correct MIME type
app.get('/downloads/:filename', (req, res) => {
    const fileName = req.params.filename;
    const filePath = path.join(downloadsDir, fileName);

    if (fs.existsSync(filePath)) {
        res.setHeader('Content-Type', 'application/vnd.android.package-archive');
        res.setHeader('Content-Disposition', `attachment; filename="${fileName}"`);
        return res.sendFile(filePath);
    } else {
        return res.status(404).send(`
            <div style="font-family: sans-serif; text-align: center; padding: 50px;">
                <h1 style="color: #751A38;">File Not Found / फ़ाइल नहीं मिली</h1>
                <p>The requested file <code>${fileName}</code> was not found in <code>website/downloads/</code> folder.</p>
                <p>Please place your <code>CollegeIdManagement.apk</code> inside <code>website/downloads/</code> directory on your VPS server.</p>
                <a href="/" style="color: #B89135; font-weight: bold;">← Back to Home / वापस जाएँ</a>
            </div>
        `);
    }
});

app.get('*', (req, res) => {
    res.sendFile(path.join(__dirname, 'index.html'));
});

app.listen(PORT, () => {
    console.log(`=======================================================`);
    console.log(`🚀 College ID Download Server is running on port ${PORT}`);
    console.log(`🌐 Local URL: http://localhost:${PORT}`);
    console.log(`=======================================================`);
});
