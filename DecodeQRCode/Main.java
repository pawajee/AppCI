package org.example;
import boofcv.abst.fiducial.QrCodeDetector;
import boofcv.alg.fiducial.qrcode.QrCode;
//import boofcv.app.batch.BatchControlPanel;
import boofcv.factory.fiducial.FactoryFiducial;
import boofcv.io.image.ConvertBufferedImage;
import boofcv.struct.image.GrayU8;
import java.awt.image.BufferedImage;
import java.awt.image.DataBufferByte;
import java.io.*;
import java.nio.file.Path;
import java.nio.file.Paths;

import org.opencv.core.*;
import org.opencv.videoio.VideoCapture;

public class Main {
    public static QrCodeDetector<GrayU8> scanner = FactoryFiducial.qrcode(null, GrayU8.class);
    static{
      //  System.loadLibrary(Core.NATIVE_LIBRARY_NAME);
        //nu.pattern.OpenCV.loadShared();
        nu.pattern.OpenCV.loadLocally();
    }

    public static void main(String[] args) throws IOException {
/*        System.out.println("Welcome to OpenCV " + Core.VERSION);
        Mat m = new Mat(5, 10, CvType.CV_8UC1, new Scalar(0));
        System.out.println("OpenCV Mat: " + m);
        Mat mr1 = m.row(1);
        mr1.setTo(new Scalar(1));
        Mat mc5 = m.col(5);
        mc5.setTo(new Scalar(5));*/
        try {
            System.out.println(Paths.get(".").toAbsolutePath().normalize().toString());
            System.out.println(args[0]);
            if(args.length == 0)
            {
                System.out.println("Please specify the video file to process!");
                return;
            }
            if(!(new File(args[0])).exists()){
                System.out.println("file not found:"+args[0]);
                return;
            }
           captureFrames(args[0]);
        }
        catch (Exception e)
        {

            System.out.println(e.getMessage());
            //return -1;
        }
        //return  0;
       // System.out.println("OpenCV Mat data:\n" + m.dump());
    }
    public static BufferedImage Mat2BufferedImage(Mat mat) throws IOException{
        //Encoding the image
        int type = BufferedImage.TYPE_BYTE_GRAY;
        if (mat.channels() > 1) {
            type = BufferedImage.TYPE_3BYTE_BGR;
        }
       // Imgproc.cvtColor(mat, mat, Imgproc.COLOR_RGB2GRAY, 0);

        // Create an empty image in matching format
        BufferedImage gray = new BufferedImage(mat.width(), mat.height(),type);// BufferedImage.TYPE_BYTE_GRAY);

        // Get the BufferedImage's backing array and copy the pixels directly into it
        byte[] data = ((DataBufferByte) gray.getRaster().getDataBuffer()).getData();
        mat.get(0, 0, data);
        return gray;
        /*
        MatOfByte matOfByte = new MatOfByte();
        Imgcodecs.imencode(".jpg", mat, matOfByte);
        //Storing the encoded Mat in a byte array
        byte[] byteArray = matOfByte.toArray();
        //Preparing the Buffered Image
        InputStream in = new ByteArrayInputStream(byteArray);
        return ImageIO.read(in);
        */

    }
    public static void captureFrames(String path) throws IOException {
        // Load the OpenCV native library
        try{
            System.loadLibrary(Core.NATIVE_LIBRARY_NAME);
        } catch (Exception e) {
            System.out.println(e.getMessage());
            throw new RuntimeException(e);
        }

        // Path to video file
        VideoCapture vidObj = new VideoCapture(path);

        // Used as counter variable
        int count = 0,total=0;

        // checks whether frames were extracted
        boolean success = true;
        PrintStream output = null;
        int currentFileFrameNo=0;
        //string missingSnippetNo;
        StringBuilder missingSnippetNos = new StringBuilder();
        String cMissingFrames="",lastFileName="",totalSnippets="";
        int cSnippetIndex=0,lSnippetIndex=-1;
        while (success) {
            // vidObj object calls read function to extract frames
            Mat image = new Mat();
            success = vidObj.read(image);
            if(!(image.width() > 0 && image.height() >0)){
                // success=false;
                total++;
                System.out.println("zero height width, frame no:"+total);
                continue;
            }
            // System.out.println(image.dump());
            BufferedImage buffered = Mat2BufferedImage(image);
            GrayU8 gray = new GrayU8(1, 1);
            ConvertBufferedImage.convertFrom(buffered, gray);
            scanner.process(gray);
            String snippet="";
            String[] arrFileInfo=null;
            for (QrCode qr : scanner.getDetections()) {
                snippet=qr.message;
                arrFileInfo=snippet.substring(0, snippet.indexOf('$')).split("/");
                // output.println(URLEncoder.encode(qr.message, "UTF-8"));
            }

            if((arrFileInfo != null) ){
                cSnippetIndex=Integer.parseInt(arrFileInfo[0]);
                if(cSnippetIndex==lSnippetIndex) // same qr as previous iteration
                {
                    continue;
                }
                else if(cSnippetIndex==0) {//new file: first qr code of a file
                    if(!cMissingFrames.isEmpty()){ // save missing frame number along with file name
                        missingSnippetNos.append(lastFileName+":" +cMissingFrames+"\r\n");
                        cMissingFrames="";
                    }
                    lastFileName=arrFileInfo[2];
                    output = new PrintStream(lastFileName);
                    System.out.println("New File:" + arrFileInfo[2]+", total qr codes:"+arrFileInfo[1]);

                }

                else if( cSnippetIndex >=(lSnippetIndex+1) )// new qr code found
                {
                    //  if(cSnippetIndex ==(lSnippetIndex+1))
                    if (cSnippetIndex > (lSnippetIndex + 1)) {
                        var missingFramesCount = cSnippetIndex - (lSnippetIndex + 1);
                        for (var i = 0; i < missingFramesCount; i++) {
                            int missingFrameNo = lSnippetIndex + 1 + i;
                            cMissingFrames += missingFrameNo+",";

                        }
                    }
                    //output.println( snippet.substring(snippet.indexOf('$'),snippet.length()-1));
                    output.println( snippet);
                }

                lSnippetIndex= cSnippetIndex;
            }

            total++;
            if(total%100 ==0)
            {
                System.out.println("number of frames processed:"+total+", current qr code:"+lSnippetIndex);
            }
 /*           if (success) {
                // Saves the frames with frame-count
                Imgcodecs.imwrite(String.format(".\\vidoes\\frames\\frame%012d.jpg", count), image);
                count++;
            }*/
        }
        var outputMissingNos = new PrintStream("MissingNos.txt");
        outputMissingNos.print(missingSnippetNos);
    }
    public static void captureMissingFrames(String path,String MissingFrames) throws IOException {
        // Load the OpenCV native library
        try{
            System.loadLibrary(Core.NATIVE_LIBRARY_NAME);
        } catch (Exception e) {
            System.out.println(e.getMessage());
            throw new RuntimeException(e);
        }

        // Path to video file
        VideoCapture vidObj = new VideoCapture(path);

        // Used as counter variable
        int count = 0,total=0;

        // checks whether frames were extracted
        boolean success = true;
        PrintStream output = null;
        int currentFileFrameNo=0;
        //string missingSnippetNo;
        StringBuilder missingSnippetNos = new StringBuilder();
        String cMissingFrames="",lastFileName="",totalSnippets="";
        int cSnippetIndex=0,lSnippetIndex=-1;
        while (success) {
            // vidObj object calls read function to extract frames
            Mat image = new Mat();
            success = vidObj.read(image);
            if(!(image.width() > 0 && image.height() >0)){
               // success=false;
                total++;
                System.out.println("zero height width, frame no:"+total);
                continue;
            }
          // System.out.println(image.dump());
            BufferedImage buffered = Mat2BufferedImage(image);
            GrayU8 gray = new GrayU8(1, 1);
            ConvertBufferedImage.convertFrom(buffered, gray);
            scanner.process(gray);
            String snippet="";
            String[] arrFileInfo=null;
            for (QrCode qr : scanner.getDetections()) {
                 snippet=qr.message;
                 arrFileInfo=snippet.substring(0, snippet.indexOf('$')).split("/");
               // output.println(URLEncoder.encode(qr.message, "UTF-8"));
            }

            if((arrFileInfo != null) ){
                cSnippetIndex=Integer.parseInt(arrFileInfo[0]);
                if(cSnippetIndex==lSnippetIndex) // same qr as previous iteration
                {
                    continue;
                }
                else if(cSnippetIndex==0) {//new file: first qr code of a file
                    if(!cMissingFrames.isEmpty()){ // save missing frame number along with file name
                        missingSnippetNos.append(lastFileName+":" +cMissingFrames+"\r\n");
                        cMissingFrames="";
                    }
                    lastFileName=arrFileInfo[2];
                    output = new PrintStream(lastFileName);
                    System.out.println("New File:" + arrFileInfo[2]+", total qr codes:"+arrFileInfo[1]);

                }

                else if( cSnippetIndex >=(lSnippetIndex+1) )// new qr code found
                {
                    //  if(cSnippetIndex ==(lSnippetIndex+1))
                    if (cSnippetIndex > (lSnippetIndex + 1)) {
                        var missingFramesCount = cSnippetIndex - (lSnippetIndex + 1);
                        for (var i = 0; i < missingFramesCount; i++) {
                            int missingFrameNo = lSnippetIndex + 1 + i;
                            cMissingFrames += missingFrameNo+",";

                        }
                    }
                    //output.println( snippet.substring(snippet.indexOf('$'),snippet.length()-1));
                    output.println( snippet);
                }

                lSnippetIndex= cSnippetIndex;
            }

            total++;
            if(total%100 ==0)
            {
                System.out.println("number of frames processed:"+total+", current qr code:"+lSnippetIndex);
            }
 /*           if (success) {
                // Saves the frames with frame-count
                Imgcodecs.imwrite(String.format(".\\vidoes\\frames\\frame%012d.jpg", count), image);
                count++;
            }*/
        }
        var outputMissingNos = new PrintStream("MissingNos.txt");
        outputMissingNos.print(missingSnippetNos);
    }
    public static String getFileExtension(String fileName) {
        int index = fileName.lastIndexOf('.');
        if (index == -1) {
            return "";
        } else {
            return fileName.substring( index+1,fileName.length()-1);
        }
    }
    public static String getBaseName(String fileName) {
        int index = fileName.lastIndexOf('.');
        if (index == -1) {
            return fileName;
        } else {
            return fileName.substring(0, index);
        }
    }
/*    public static void processFrame(){

        ConvertBufferedImage.convertFrom(buffered, gray);

        scanner.process(gray);
        output.printf("%d %s\n", scanner.getDetections().size(), f.getPath());
    }*/
}
