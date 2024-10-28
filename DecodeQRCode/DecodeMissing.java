
import org.opencv.core.Core;
import org.opencv.core.CvType;
import org.opencv.core.Mat;
import boofcv.abst.fiducial.QrCodeDetector;
import boofcv.alg.fiducial.qrcode.QrCode;
//import boofcv.app.batch.BatchControlPanel;
import boofcv.factory.fiducial.FactoryFiducial;
import boofcv.io.image.ConvertBufferedImage;
import boofcv.struct.image.GrayU8;
import java.awt.image.BufferedImage;
import java.awt.image.DataBufferByte;
import java.io.*;
import java.util.*;
import java.nio.file.Path;
import java.nio.file.Paths;

import org.opencv.core.*;
import org.opencv.videoio.VideoCapture;

/**
 * Write a description of class Main here.
 *
 * @author (your name)
 * @version (a version number or a date)
 */
public class AddMissingQRCodes
{
    public static QrCodeDetector<GrayU8> scanner = FactoryFiducial.qrcode(null, GrayU8.class);
    static{
      //  System.loadLibrary(Core.NATIVE_LIBRARY_NAME);
        //nu.pattern.OpenCV.loadShared();
       // nu.pattern.OpenCV.loadLocally();
    }


    /**
     * Constructor for objects of class Main
     */
    public static void main(String[] args){
    
		if(args.length <2){
			
			logln("Please pass the video and missing list file");
			//return;
		}
		var videoFile=args[0];
		var missingListFile=args[1];
		if(!fileExist(videoFile)){
			logln("Vidoe File not found:"+ videoFile);
			//return;
		}
		if(!fileExist(missingListFile)){
			logln("Missing List File not found:"+ missingListFile);			
			//return;
		}
		missingListFile="MissingNos.txt";
		videoFile="test.mp4";
		decodeMissingFrames(videoFile,missingListFile);
	/*
		System.loadLibrary(Core.NATIVE_LIBRARY_NAME);
		Mat mat = Mat.eye(3, 3, CvType.CV_8UC1);
		System.out.println("mat = " + mat.dump());
		try{
			int key = System.in.read();
		}
		catch(Exception ex){
			
			
		}
		*/
    }
    
    	public static void dLog(String msg){
		System.out.println(msg);
		
	}
	public static void logln(String msg){
		System.out.println(msg);
		
	}
	public static void log(String msg){
		System.out.print(msg);
		
	}	
	public static boolean fileExist(String path){
		return (new File(path)).exists();
	}
	
	public static void decodeMissingFrames(String videoFile,String missingListFile){
		        // Load the OpenCV native library
        	StringBuilder missingNos = new StringBuilder();
            try{
            
            	var missingList=getMissingList(missingListFile);
            	for(var fileInfo: missingList) {
            		var arrFileInfo=fileInfo.split(":");
            		var fileName=arrFileInfo[0].trim();
            		var chunkNos=arrFileInfo[1].split(",");									
            		
            		var missingNo=decodeMissingFrame(fileName,arrFileInfo);
            		if(missingNo!=null){
                	  missingNos.append(missingNo+"\r\n");
            		}
            	}
            	if (!missingNos.toString().equals(""))
                {
                    var outputMissingNos = new PrintStream(getNextFileNo("","MissingNos",".txt"));
                    outputMissingNos.print(missingNos);
                }
 
            } 
            catch (Exception e) {
                System.out.println(e.getMessage());
                throw new RuntimeException(e);
            }
		
	}
	public static String decodeMissingFrame(String fileName, String[] chunkNos){
		
            try{
            	System.loadLibrary(Core.NATIVE_LIBRARY_NAME);
            	VideoCapture vidObj = new VideoCapture(fileName);
            		
            	int count = 0,total=0;
            	// checks whether frames were extracted
            	boolean success = true;
            	int currentFileFrameNo=0,lastFileFrameNo=-1;
            	//string missingSnippetNo;
            	StringBuilder missingSnippetNos = new StringBuilder();
            	StringBuilder MissingQrCodes = new StringBuilder();
            	List<String> AddedQrSnippetNos = new ArrayList<String>();
            	String cMissingFrames="",lastFileName="",totalSnippets="";
            	int cSnippetIndex=0,lSnippetIndex=-1;
            	while (success) {
            		// vidObj object calls read function to extract frames
            		Mat image = new Mat();
            		success = vidObj.read(image);
            		dLog("Frame read!");
            		if(!(image.width() > 0 && image.height() >0)){
            		   // success=false;
            			total++;
            			System.out.println("zero height width, frame no:"+total);
            			continue;
            		}
            		dLog("found proper frame!");
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
            			 dLog(snippet);
            		   // output.println(URLEncoder.encode(qr.message, "UTF-8"));
            		}
                        dLog("arrFileInfo:"+ String.join(","));
            		if((arrFileInfo != null) ){
            			var strCurSnippetIndex=arrFileInfo[0];
            			cSnippetIndex=Integer.parseInt(strCurSnippetIndex);
            			 dLog("strCurSnippetIndex:"+strCurSnippetIndex+ ", lSnippetIndex:"+lSnippetIndex);
            			if(cSnippetIndex==lSnippetIndex) // same qr as previous iteration
            			{
            				continue;
            			}
            			//else if( cSnippetIndex >=(lSnippetIndex+1) )// new qr code found
            			else if( Arrays.binarySearch(chunkNos, strCurSnippetIndex) > -1)
            			{
            				boolean found = Arrays.binarySearch(chunkNos, strCurSnippetIndex) > -1;
            				//  if(cSnippetIndex ==(lSnippetIndex+1))
            				if (found) {
            					MissingQrCodes.append(snippet+"\r\n");
            					AddedQrSnippetNos.add(strCurSnippetIndex);
            
            				}
            				//output.println( snippet.substring(snippet.indexOf('$'),snippet.length()-1));
            			   
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
            	}//end of while loop
            	appendTextTofile(fileName+".txt",MissingQrCodes.toString());
            	if(AddedQrSnippetNos.size()<chunkNos.length){
                	for(var addedQrNo:chunkNos){
                    	   // var found =Arrays.binarySearch(chunkNos, addedQrNo);
                    	   // var found =AddedQrSnippetNos.contains();
                            if(!AddedQrSnippetNos.contains(addedQrNo)){
                                missingSnippetNos.append(addedQrNo+",");
                            }
                	}
            	}
            	//return fileName+":"+missingSnippetNos;
            	return missingSnippetNos.toString();
            } 
            catch (Exception e) {
                dLog("Exception in decodeMissingFrame");
                System.out.println(e.getMessage());
                throw new RuntimeException(e);
            }
           // return "";
	}
	public static void appendTextTofile(String filePath, String textToAppend){
    	try (BufferedWriter writer = new BufferedWriter(new FileWriter(filePath, true))) {
                writer.write(textToAppend);
             //   writer.newLine(); // Add a new line if desired
            } catch (IOException e) {
                System.out.println("An error occurred: " + e.getMessage());
            }
        }
	public static String getNextFileNo(String path,String fileName,String ext){
            int num = 0;
            String fFileName = fileName+"_"+ num + ext;
            File file = new File(path, fFileName);
            while(file.exists()) {
                fFileName = fileName+"_"+ ++num  + ext;
                file = new File(path, fFileName);
            }
            return fFileName;
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
	public static List<String> getMissingList(String missingListFile){
		BufferedReader reader;
		List<String> missingList = new ArrayList<>();
		try {
			reader = new BufferedReader(new FileReader(missingListFile));
			String line = reader.readLine();

			while (line != null) {
				missingList.add(line);
				// read next line
				line = reader.readLine();
			}
			reader.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
		return missingList;
		
	}
}
